using GDE.Pedidos.API.Models;

namespace GDE.Pedidos.API.DTO
{
    public class BuscarPedidoItemDTO
    {
        public Guid ProdutoId { get; set; }
        public LocalDTO? Local { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public Guid? PedidoCompraId { get; set; }
        public Guid? PedidoVendaId { get; set; }
        public Guid? PedidoTransferenciaId { get; set; }
        public string? DataValidade { get; set; }

        public static BuscarPedidoItemDTO FromDomain(PedidoItem pedidoItem) =>
            new BuscarPedidoItemDTO
            {
                ProdutoId = pedidoItem.ProdutoId,
                Local = new(pedidoItem.LocalId, pedidoItem.LocalNome),
                Quantidade = pedidoItem.Quantidade,
                PrecoUnitario = pedidoItem.PrecoUnitario,
                PedidoCompraId = pedidoItem.PedidoCompraId,
                PedidoVendaId = pedidoItem.PedidoVendaId,
                PedidoTransferenciaId = pedidoItem.PedidoTransferenciaId,
                DataValidade = pedidoItem.DataValidade.HasValue ? pedidoItem.DataValidade.Value.ToString("dd/MM/yyyy") : string.Empty
            };
    }
}
