using GDE.Core.DomainObjects;

namespace GDE.Pedidos.API.Models
{
    public class PedidoItem : Entity
    {
        public PedidoItem(Guid produtoId, Guid localId, int quantidade, decimal precoUnitario,
            Guid? pedidoCompraId, Guid? pedidoVendaId, Guid? pedidoTransferenciaId, DateTime dataValidade)
        {
            ProdutoId = produtoId;
            LocalId = localId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            PedidoCompraId = pedidoCompraId;
            PedidoVendaId = pedidoVendaId;
            PedidoTransferenciaId = pedidoTransferenciaId;
            DataValidade = dataValidade;
        }

        public Guid ProdutoId { get; set; }
        public Guid LocalId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public Guid? PedidoCompraId { get; set; }
        public Guid? PedidoVendaId { get; set; }
        public Guid? PedidoTransferenciaId { get; set; }
        public DateTime DataValidade { get; set; }

        //EF Relations
        public PedidoCompra PedidoCompra { get; set; }
        public PedidoVenda PedidoVenda { get; set; }
        public PedidoTransferencia PedidoTransferencia { get; set; }
    }
}
