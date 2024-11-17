using GDE.Core.DomainObjects;

namespace GDE.Pedidos.API.Models
{
    public class PedidoItem : Entity
    {
        public PedidoItem(Guid produtoId, Guid localId, string? localNome, int quantidade, decimal precoUnitario,
            Guid? pedidoCompraId, Guid? pedidoVendaId, Guid? pedidoTransferenciaId, DateTime dataValidade, string? imagem)
        {
            ProdutoId = produtoId;
            LocalId = localId;
            LocalNome = localNome;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
            PedidoCompraId = pedidoCompraId;
            PedidoVendaId = pedidoVendaId;
            PedidoTransferenciaId = pedidoTransferenciaId;
            DataValidade = dataValidade;
            Imagem = imagem;
        }

        public Guid ProdutoId { get; set; }
        public Guid LocalId { get; set; }
        public string? LocalNome { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public DateTime DataValidade { get; set; }
        public string? Imagem { get; set; }
        public Guid? PedidoCompraId { get; set; }
        public Guid? PedidoVendaId { get; set; }
        public Guid? PedidoTransferenciaId { get; set; }

        //EF Relations
        public PedidoCompra PedidoCompra { get; set; }
        public PedidoVenda PedidoVenda { get; set; }
        public PedidoTransferencia PedidoTransferencia { get; set; }
    }
}
