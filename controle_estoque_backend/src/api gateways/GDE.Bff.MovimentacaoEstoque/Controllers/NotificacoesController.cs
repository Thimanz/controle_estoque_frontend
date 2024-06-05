using GDE.Bff.MovimentacaoEstoque.Models;
using GDE.Bff.MovimentacaoEstoque.Services;
using GDE.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Bff.MovimentacaoEstoque.Controllers
{
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

            foreach (var item in vencimento)
            {
                var produto = await _produtoService.ObterProdutoPorId(item.Id);
                
                item.Nome = produto.Nome;
                item.Mensagem = $"Existem itens do produto {produto.Nome} próximos ao vencimento";
                item.Tipo = TipoNotificacao.ProximoVencimento;
            }

            notificacoes.AddRange(vencimento);

            return CustomResponse(notificacoes);
        }
    }
}
