using GDE.Core.DomainObjects;

namespace GDE.Pedidos.API.Models
{
    public class PedidoTransferencia : Pedido, IAggregateRoot
    {
        public PedidoTransferencia(Guid idLocalDestino, Guid idFuncionarioResponsavel, List<PedidoItem> pedidoItens)
            : base(idFuncionarioResponsavel, pedidoItens)
        {
            IdLocalDestino = idLocalDestino;
        }

        public Guid IdLocalDestino { get; private set; }

        public PedidoTransferencia() : base() { }
    }
}
