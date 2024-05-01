using GDE.Core.Controllers;
using GDE.Core.Mediator;
using GDE.Core.Usuario;
using GDE.Pedidos.API.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Pedidos.API.Controllers
{
    //[Authorize]
    public class PedidoController : MainController
    {
        private readonly IMediatorHandler _mediator;
        
        public PedidoController(IMediatorHandler mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("api/pedido/compra")]
        public async Task<IActionResult> AdicionarPedidoCompra(AdicionarPedidoCompraCommand pedido)
        {
            return CustomResponse(await _mediator.EnviarComando(pedido));
        }
    }
}
