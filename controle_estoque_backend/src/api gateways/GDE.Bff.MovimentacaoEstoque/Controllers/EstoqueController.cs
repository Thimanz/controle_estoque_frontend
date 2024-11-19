using GDE.Bff.MovimentacaoEstoque.Services;
using GDE.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Bff.MovimentacaoEstoque.Controllers
{
    public class EstoqueController : MainController
    {
        private readonly IEstoqueService _estoqueService;
        private readonly IProdutoService _produtoService;

        public EstoqueController(IEstoqueService estoqueService, IProdutoService produtoService)
        {
            _estoqueService = estoqueService;
            _produtoService = produtoService;
        }

        [HttpGet("api/estoque/{id}")]
        public async Task<IActionResult> ObterPedidoCompra(Guid id)
        {
            var local = await _estoqueService.ObterLocalPorId(id);

            if (local is null)
                return CustomResponse();

            foreach (var item in local.LocalItens)
            {
                var produto = await _produtoService.ObterProdutoPorId(item.ProdutoId);
                item.Imagem = produto?.Imagem;
                item.DataValidade = string.IsNullOrEmpty(item.DataValidade)
                    ? item.DataValidade
                    : DateTimeOffset.Parse(item.DataValidade).ToString("dd/MM/yyyy");
            }

            return CustomResponse(local);
        }

    }
}
