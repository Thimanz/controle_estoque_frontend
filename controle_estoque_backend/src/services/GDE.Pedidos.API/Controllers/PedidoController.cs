using GDE.Core.Controllers;
using GDE.Core.Mediator;
using GDE.Core.Usuario;
using GDE.Pedidos.API.Application.Commands;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Pedidos.API.Controllers
{
    public class PedidoController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly IAspNetUser _user;

        public PedidoController(IMediatorHandler mediator, IAspNetUser user)
        {
            _mediator = mediator;
            _user = user;
        }

        [HttpPost("api/pedido/compra")]
        public async Task<IActionResult> AdicionarPedidoCompra(AdicionarPedidoCompraCommand pedido)
        {
            pedido.IdFuncionarioResponsavel = _user.ObterUserId();

            return CustomResponse(await _mediator.EnviarComando(pedido));
        }
    }
}
