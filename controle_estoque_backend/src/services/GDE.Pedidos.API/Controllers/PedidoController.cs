using GDE.Core.Controllers;
using GDE.Core.Data;
using GDE.Core.Mediator;
using GDE.Pedidos.API.Application.Commands;
using GDE.Pedidos.API.Data;
using GDE.Pedidos.API.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GDE.Pedidos.API.Controllers
{
    //[Authorize]
    public class PedidoController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly PedidosContext _context;

        public PedidoController(IMediatorHandler mediator, PedidosContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet("api/pedido")]
        public async Task<PagedResult<BuscarPedidosDto>> BuscarPedidos([FromQuery] DateTime? dataCriacao, [FromQuery] int pageSize = 30, [FromQuery] int pageIndex = 1)
        {
            int pageSizeByType = pageSize / 3;

            var pedidosCompra = await _context.PedidosCompra.Include(p => p.PedidoItens)
               .Skip(pageSizeByType * (pageIndex - 1))
               .Take(pageSizeByType)
               .Where(p => !dataCriacao.HasValue || p.DataCriacao.Date == dataCriacao.Value.Date).ToListAsync();

            var pedidosVenda = await _context.PedidosVenda.Include(p => p.PedidoItens)
               .Skip(pageSizeByType * (pageIndex - 1))
               .Take(pageSizeByType)
               .Where(p => !dataCriacao.HasValue || p.DataCriacao.Date == dataCriacao.Value.Date).ToListAsync();

            var pedidosTransferencia = await _context.PedidosTransferencia.Include(p => p.PedidoItens)
               .Skip(pageSizeByType * (pageIndex - 1))
               .Take(pageSizeByType)
               .Where(p => !dataCriacao.HasValue || p.DataCriacao.Date == dataCriacao.Value.Date).ToListAsync();

            var pedidos = pedidosCompra.Select(BuscarPedidosDto.FromPedidoCompra).ToList();

            pedidos.AddRange(pedidosVenda.Select(BuscarPedidosDto.FromPedidoVenda));
            pedidos.AddRange(pedidosTransferencia.Select(BuscarPedidosDto.FromPedidoTransferencia));

            return new PagedResult<BuscarPedidosDto>()
            {
                List = pedidos,
                TotalResults = pedidos.Count(),
                TotalPages = ((pedidos.Count() + pageSize - 1) / pageSize),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
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
