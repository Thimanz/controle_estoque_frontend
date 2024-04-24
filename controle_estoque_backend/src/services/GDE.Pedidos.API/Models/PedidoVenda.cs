namespace GDE.Pedidos.API.Models
{
    public class PedidoVenda : Pedido
    {
        public PedidoVenda(Guid idFuncinarioResposavel, string? nomeCliente, List<PedidoItem> pedidoItens)
            : base(idFuncinarioResposavel, pedidoItens)
        {
            NomeCliente = nomeCliente;
        }

        public string? NomeCliente { get; private set; }
    }
}
