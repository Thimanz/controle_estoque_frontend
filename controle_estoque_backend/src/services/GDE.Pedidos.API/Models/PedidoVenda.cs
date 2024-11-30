using GDE.Core.DomainObjects;

namespace GDE.Pedidos.API.Models
{
    public class PedidoVenda : Pedido, IAggregateRoot
    {
        public PedidoVenda(string? nomeCliente, DateTime dataCriacao, Guid idFuncionarioResponsavel, List<PedidoItem> pedidoItens)
            : base(idFuncionarioResponsavel, dataCriacao, pedidoItens)
        {
            NomeCliente = nomeCliente;
        }

        public PedidoVenda() { }

        public string? NomeCliente { get; private set; }
    }
}
