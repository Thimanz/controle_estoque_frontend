using GDE.Core.Controllers;
using GDE.Core.Data;
using GDE.Produtos.API.Data;
using GDE.Produtos.API.Entities;
using GDE.Produtos.API.Models.InputModels;
using GDE.Produtos.API.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GDE.Produtos.API.Controllers
{
    [Authorize]
    public class ProdutosController : MainController
    {
        private readonly ProdutoContext _context;

        public ProdutosController(ProdutoContext context)
        {
            _context = context;
        }

        [HttpGet("api/produto/{id}")]
        public async Task<IActionResult> ProdutoDetalhe(Guid id)
        {
            var produto = await _context.Produtos.Include(p => p.Categoria).FirstOrDefaultAsync(p => p.Id == id);

            if (produto is null)
                return NotFound();

            var produtoViewModel = ProdutoViewModel.FromEntity(produto);

            return CustomResponse(produtoViewModel);
        }

        [HttpGet("api/produto/lista-por-nome")]
        public async Task<IActionResult> ListaProdutosPorNome([FromQuery] string nome, [FromQuery] int pageSize = 30, [FromQuery] int pageIndex = 1)
        {
            var produtos = await _context.Produtos.Include(p => p.Categoria)
                .OrderBy(p => p.Nome)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize)
                .Where(p => p.Nome!.ToLower().Contains(nome.ToLower())).ToListAsync();

            return !produtos.Any()
                ? NotFound()
                : CustomResponse(new PagedResult<ProdutoViewModel>()
                {
                    List = produtos.Select(ProdutoViewModel.FromEntity),
                    TotalResults = produtos.Count(),
                    TotalPages = ((produtos.Count() + pageSize - 1) / pageSize),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });
        }

        [HttpGet("api/produto/listar-todos")]
        public async Task<IActionResult> ListaProdutos([FromQuery] int pageSize = 30, [FromQuery] int pageIndex = 1)
        {
            var produtos = await _context.Produtos.Include(p => p.Categoria)
                .OrderBy(p => p.Nome)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize).ToListAsync();

            return !produtos.Any()
                ? NotFound()
                : CustomResponse(new PagedResult<ProdutoViewModel>()
                {
                    List = produtos.Select(ProdutoViewModel.FromEntity),
                    TotalResults = produtos.Count(),
                    TotalPages = ((produtos.Count() + pageSize - 1) / pageSize),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });
        }

        [HttpGet("api/produto/categorias")]
        public IActionResult ListaCategorias()
        {
            var categorias = _context.Categorias.Select(CategoriaViewModel.FromEntity);

            return !categorias.Any() ? NotFound() : CustomResponse(categorias);
        }

        [HttpGet("api/produto/nivel-estoque-baixo")]
        public async Task<IActionResult> ProdutosNivelEstoqueBaixo()
        {
            var produtos = await _context.Produtos.Include(p => p.Categoria).Where(p => p.QuantidadeEstoque <= p.NivelMinimoEstoque).ToListAsync();

            var viewModels = new List<ProdutosEstoqueBaixoViewModel>();

            foreach (var produto in produtos)
            {
                viewModels.Add(new ProdutosEstoqueBaixoViewModel(
                    produto.Id,
                    produto.Nome,
                    $"O nível de estoque do produto {produto.Nome} está baixo"
                ));
            }

            return CustomResponse(viewModels);
        }

        [HttpPost("api/produto")]
        public async Task<IActionResult> AdicionarProduto(ProdutoInputModel produtoInputModel)
        {
            var produto = produtoInputModel.ToEntity();

            var categoria = await _context.Categorias.FindAsync(produto.CategoriaId);

            if (categoria is null)
            {
                AdicionarErroProcessamento("Categoria não encontrada");
                return CustomResponse();
            }

            ValidarProduto(produto);
            if (!OperacaoValida()) return CustomResponse();

            _context.Produtos.Add(produto);

            await PersistirDados();
            return CustomResponse();
        }

        [HttpPut("api/produto/{produtoId}")]
        public async Task<IActionResult> AtualizarProduto(Guid produtoId, ProdutoInputModel produtoInputModel)
        {
            var produto = produtoInputModel.ToEntity();

            var produtoExistente = await _context.Produtos.FindAsync(produtoId);

            if (produtoExistente is null)
            {
                AdicionarErroProcessamento("Produto não encontrado");
                return CustomResponse();
            }

            produto.Id = produtoId;

            ValidarProduto(produto);
            if (!OperacaoValida()) return CustomResponse();

            _context.Produtos.Update(produto);
            _context.Entry(produto).Property(x => x.QuantidadeEstoque).IsModified = false;
            _context.Entry(produto).Property(x => x.DataCadastro).IsModified = false;

            await PersistirDados();
            return CustomResponse();
        }

        [HttpDelete("api/produto/{produtoId}")]
        public async Task<IActionResult> RemoverProduto(Guid produtoId)
        {
            var produtoExistente = await _context.Produtos.FindAsync(produtoId);

            if (produtoExistente is null)
            {
                AdicionarErroProcessamento("Produto não encontrado");
                return CustomResponse();
            }

            _context.Remove(produtoExistente);
            await PersistirDados();

            return CustomResponse();
        }

        private bool ValidarProduto(Produto produto)
        {
            if (produto.IsValid()) return true;

            produto.ValidationResult!.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
            return false;
        }

        private async Task PersistirDados()
        {
            var commited = await _context.Commit();
            if (!commited) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        }
    }
}
