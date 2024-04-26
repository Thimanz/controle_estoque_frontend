using GDE.Pedidos.API.Models;

namespace GDE.Pedidos.API.Application.DTO
{
    public class PedidoItemDTO
    {
        public Guid? PedidoCompraId { get; set; }
        public Guid? PedidoVendaId { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid LocalId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }

        public static PedidoItem ParaPedidoItem(PedidoItemDTO pedidoItemDTO)
        {
            var pedidoItem = new PedidoItem(pedidoItemDTO.ProdutoId, pedidoItemDTO.LocalId, 
                pedidoItemDTO.Quantidade, pedidoItemDTO.PrecoUnitario, pedidoItemDTO.PedidoCompraId, pedidoItemDTO.PedidoVendaId);

            return pedidoItem;
        }
    }
}
