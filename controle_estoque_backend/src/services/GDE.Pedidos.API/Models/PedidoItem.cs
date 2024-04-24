namespace GDE.Pedidos.API.Models
{
    public class PedidoItem
    {
        public PedidoItem(Guid pedidoId, Guid produtoId, Guid localId, int quantidade, decimal precoUnitario)
        {
            Id = Guid.NewGuid();
            PedidoId = pedidoId;
            ProdutoId = produtoId;
            LocalId = localId;
            Quantidade = quantidade;
            PrecoUnitario = precoUnitario;
        }

        public Guid Id { get; set; }
        public Guid PedidoId { get; set; }
        public Guid ProdutoId { get; set; }
        public Guid LocalId { get; set; }
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
    }
}
