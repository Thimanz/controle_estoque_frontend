namespace GDE.Pedidos.API.Models
{
    public class PedidoCompra : Pedido
    {
        public PedidoCompra(Guid idFuncinarioResposavel, string nomeFornecedor, List<PedidoItem> pedidoItens)
            : base(idFuncinarioResposavel, pedidoItens)
        {
            NomeFornecedor = nomeFornecedor;
        }

        public string? NomeFornecedor { get; private set; }
  
    }
}
