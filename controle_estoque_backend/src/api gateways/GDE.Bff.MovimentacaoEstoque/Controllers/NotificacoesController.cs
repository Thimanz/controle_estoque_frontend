using GDE.Bff.MovimentacaoEstoque.Models;
using GDE.Bff.MovimentacaoEstoque.Services;
using GDE.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Bff.MovimentacaoEstoque.Controllers
{
    [Authorize]
    public class NotificacoesController : MainController
    {
        private readonly IProdutoService _produtoService;
        private readonly IPedidoService _pedidoService;

        public NotificacoesController(IProdutoService produtoService, IPedidoService pedidoService)
        {
            _produtoService = produtoService;
            _pedidoService = pedidoService;
        }

        [HttpGet("api/notificacoes")]
        public async Task<IActionResult> ListaNotificacoes()
        {
            var notificacoes = (await _produtoService.ObterNotificacoesEstoqueBaixo()).ToList();

            notificacoes.ForEach(n => n.Tipo = TipoNotificacao.EstoqueBaixo);

            var vencimento = await _pedidoService.ObterNotificacoesProximoVencimento();

            var vencimentoPopulada = new List<NotificacaoDTO>();

            foreach (var item in vencimento)
            {
                var produto = await _produtoService.ObterProdutoPorId(item.IdProduto);
                
                if (produto?.Quantidade > 0)
                    vencimentoPopulada.Add(new NotificacaoDTO(
                        item.IdLocal,
                        TipoNotificacao.ProximoVencimento, 
                        produto.Nome, 
                        $"Existem itens do produto {produto.Nome} próximos ao vencimento"));
            }

            notificacoes.AddRange(vencimentoPopulada);

            return CustomResponse(notificacoes);
        }
    }
}
