using GDE.Core.Controllers;
using GDE.Core.Identidade;
using GDE.Produtos.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Produtos.API.Controllers
{
    [Authorize]
    [Route("api/produto")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;

        public ProdutosController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [ClaimsAuthorize("Produto", "Ler")]
        [HttpGet]
        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _produtoRepository.ObterTodos();
        }

        [ClaimsAuthorize("Produto", "Ler")]
        [HttpGet("/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id)
        {
            return await _produtoRepository.ObterPorId(id);
        }

        [ClaimsAuthorize("Produto", "Adicionar")]
        [HttpPost]
        public async Task<IActionResult> AdicionarProduto(Produto produto)
        {
            if (!produto.IsValid())
                return CustomResponse(produto.ValidationResult);

            _produtoRepository.Adicionar(produto);

            return CustomResponse();
        }

        [ClaimsAuthorize("Produto", "Atualizar")]
        [HttpPut("/{produtoId}")]
        public async Task<IActionResult> AtualizarProduto(Guid produtoId, Produto produto)
        {
            var produtoExistente = await _produtoRepository.ObterPorId(produtoId);

            if (produtoExistente is null)
            {
                AdicionarErroProcessamento("Produto não encontrado");
                return CustomResponse();
            }

            ValidarProduto(produto);
            if (!OperacaoValida()) return CustomResponse();

            _produtoRepository.Atualizar(produto);

            return CustomResponse();
        }

        [ClaimsAuthorize("Produto", "Atualizar")]
        [HttpDelete("/{produtoId}")]
        public async Task<IActionResult> RemoverProduto(Guid produtoId)
        {
            var produtoExistente = await _produtoRepository.ObterPorId(produtoId);

            if (produtoExistente is null)
            {
                AdicionarErroProcessamento("Produto não encontrado");
                return CustomResponse();
            }

            _produtoRepository.Remover(produtoExistente);

            return CustomResponse();
        }

        private bool ValidarProduto(Produto produto)
        {
            if (produto.IsValid()) return true;

            produto.ValidationResult.Errors.ToList().ForEach(e => AdicionarErroProcessamento(e.ErrorMessage));
            return false;
        }
    }
}
