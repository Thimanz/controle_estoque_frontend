using GDE.Bff.MovimentacaoEstoque.Services;
using GDE.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Bff.MovimentacaoEstoque.Controllers
{
    [Authorize]
    public class RelatorioController : MainController
    {
        private readonly IPedidoService _pedidoService;
        private readonly IProdutoService _produtoService;

        public RelatorioController(IPedidoService pedidoService, IProdutoService produtoService)
        {
            _pedidoService = pedidoService;
            _produtoService = produtoService;
        }

        [HttpGet("api/relatorios/vendas-custos/{qtdMeses}")]
        public async Task<IActionResult> ObterRelatorioVendasCustos(int qtdMeses)
        {
            var relatorio = await _pedidoService.ObterRelatorioVendasCustos(qtdMeses);

            if (relatorio is null)
                return CustomResponse();

            return CustomResponse(relatorio);
        }

        [HttpGet("api/relatorios/top10-produtos")]
        public async Task<IActionResult> ObterRelatorioTop10Produtos()
        {
            var relatorio = await _pedidoService.ObterRelatorioTop10Produtos();

            if (relatorio is null)
                return CustomResponse();

            foreach (var item in relatorio)
            {
                var produto = await _produtoService.ObterProdutoPorId(item.ProdutoId);
                item.Produto = produto?.Nome;
            }

            return CustomResponse(relatorio);
        }
    }
}
