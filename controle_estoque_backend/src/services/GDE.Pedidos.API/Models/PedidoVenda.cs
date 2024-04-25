using GDE.Core.DomainObjects;

namespace GDE.Pedidos.API.Models
{
    public class PedidoVenda : Pedido, IAggregateRoot
    {
        public PedidoVenda(string? nomeCliente, Guid idFuncionarioResponsavel, List<PedidoItem> pedidoItens)
            : base(idFuncionarioResponsavel, pedidoItens)
        {
            NomeCliente = nomeCliente;
        }

        public PedidoVenda() { }

        public string? NomeCliente { get; private set; }
    }
}
