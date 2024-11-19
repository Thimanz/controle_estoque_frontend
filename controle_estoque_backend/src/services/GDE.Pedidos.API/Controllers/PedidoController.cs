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
    [Authorize]
    public class PedidoController : MainController
    {
        private readonly IMediatorHandler _mediator;
        private readonly PedidosContext _context;

        public PedidoController(IMediatorHandler mediator, PedidosContext context)
        {
            _mediator = mediator;
            _context = context;
        }

        [HttpGet("api/pedido/compra/{id}")]
        public async Task<IActionResult> ObterPedidoCompra(Guid id)
        {
            var pedido = await _context.PedidosCompra.Include(p => p.PedidoItens).FirstOrDefaultAsync(l => l.Id == id);
            if (pedido is null)
                return CustomResponse();

            return CustomResponse(ObterPedidoCompraDTO.FromDomain(pedido));
        }

        [HttpGet("api/pedido/venda/{id}")]
        public async Task<IActionResult> ObterPedidoVenda(Guid id)
        {
            var pedido = await _context.PedidosVenda.Include(p => p.PedidoItens).FirstOrDefaultAsync(l => l.Id == id);
            if (pedido is null)
                return CustomResponse();

            return CustomResponse(ObterPedidoVendaDTO.FromDomain(pedido));
        }

        [HttpGet("api/pedido/transferencia/{id}")]
        public async Task<IActionResult> ObterPedidoTransferencia(Guid id)
        {
            var pedido = await _context.PedidosTransferencia.Include(p => p.PedidoItens).FirstOrDefaultAsync(l => l.Id == id);
            if (pedido is null)
                return CustomResponse();

            return CustomResponse(ObterPedidoTransferenciaDTO.FromDomain(pedido));
        }

        [HttpGet("api/pedido")]
        public async Task<IActionResult> ListaPedidos([FromQuery] DateTime? data, [FromQuery] int pageSize = 30, [FromQuery] int pageIndex = 1)
        {
            List<BuscarPedidosDto> pedidos = new();

            var pedidosCompra = await _context.PedidosCompra
                .Where(p => !data.HasValue || p.DataCriacao.Date == data.Value.Date)
                .Include(p => p.PedidoItens).ToListAsync();

            var pedidosVenda = await _context.PedidosVenda
                .Where(p => !data.HasValue || p.DataCriacao.Date == data.Value.Date)
                .Include(p => p.PedidoItens).ToListAsync();

            var pedidosTransferencia = await _context.PedidosTransferencia
                .Where(p => !data.HasValue || p.DataCriacao.Date == data.Value.Date)
                .Include(p => p.PedidoItens).ToListAsync();

            pedidos.AddRange(pedidosCompra.Select(BuscarPedidosDto.FromPedidoCompra));
            pedidos.AddRange(pedidosVenda.Select(BuscarPedidosDto.FromPedidoVenda));
            pedidos.AddRange(pedidosTransferencia.Select(BuscarPedidosDto.FromPedidoTransferencia));

            var pedidosPaged = pedidos
                .OrderBy(p => p.Numero)
                .Skip(pageSize * (pageIndex - 1))
                .Take(pageSize);

            return !pedidosPaged.Any()
                ? NotFound()
                : CustomResponse(new PagedResult<BuscarPedidosDto>()
                {
                    List = pedidosPaged,
                    TotalResults = pedidos.Count(),
                    TotalPages = ((pedidos.Count() + pageSize - 1) / pageSize),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                });
        }


        [HttpGet("api/pedido/proximos-ao-vencimento")]
        public async Task<IActionResult> ProximosAoVencimento()
        {
            var compareDate = DateTime.UtcNow.AddDays(10);

            var itens = await _context.PedidoItens.AsNoTracking().Where(i => i.DataValidade < compareDate).ToListAsync();

            var viewModels = new List<ProximosAoVencimentoDTO>();

            foreach (var item in itens)
            {
                viewModels.Add(new ProximosAoVencimentoDTO(
                    item.ProdutoId,
                    item.LocalId
                ));
            }

            return CustomResponse(viewModels);
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
            foreach (var item in pedido.PedidoItens)
            {
                var itemExistente = await _context.PedidoItens
                    .FirstOrDefaultAsync(x => x.ProdutoId == item.ProdutoId
                        && x.LocalId == item.LocalId 
                        && x.PedidoCompraId.HasValue);
                item.DataValidade = itemExistente?.DataValidade;
            }

            return CustomResponse(await _mediator.EnviarComando(pedido));
        }
    }
}
