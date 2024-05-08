using GDE.Core.Controllers;
using GDE.Core.Mediator;
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

        [HttpPost("api/pedido/venda")]
        public async Task<IActionResult> AdicionarPedidoVenda(AdicionarPedidoVendaCommand pedido)
        {
            return CustomResponse(await _mediator.EnviarComando(pedido));
        }

        [HttpPost("api/pedido/transferencia")]
        public async Task<IActionResult> AdicionarPedidoTransferencia(AdicionarPedidoTransferenciaCommand pedido)
        {
            return CustomResponse(await _mediator.EnviarComando(pedido));
        }
    }
}
