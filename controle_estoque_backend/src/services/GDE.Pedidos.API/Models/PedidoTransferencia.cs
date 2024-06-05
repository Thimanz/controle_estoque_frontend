using GDE.Core.DomainObjects;

namespace GDE.Pedidos.API.Models
{
    public class PedidoTransferencia : Pedido, IAggregateRoot
    {
        public PedidoTransferencia(Guid idLocalDestino, DateTime dataCriacao, Guid idFuncionarioResponsavel, List<PedidoItem> pedidoItens)
            : base(idFuncionarioResponsavel, dataCriacao, pedidoItens)
        {
            IdLocalDestino = idLocalDestino;
        }

        public Guid IdLocalDestino { get; private set; }

        public PedidoTransferencia() : base() { }
    }
}
