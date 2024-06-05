using GDE.Bff.MovimentacaoEstoque.Services;
using GDE.Core.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Bff.MovimentacaoEstoque.Controllers
{
    public class NotificacoesController : MainController
    {
        private readonly IProdutoService _produtoService;

        public NotificacoesController(IProdutoService produtoService)
        {
            _produtoService = produtoService;
        }

        [HttpGet("api/notificacoes")]
        public async Task<IActionResult> ListaNotificacoes()
        {
            var notificacoes = await _produtoService.ObterNotificacoesEstoqueBaixo();



            return CustomResponse(notificacoes);
        }
    }
}
