using GDE.Core.DomainObjects;

namespace GDE.Pedidos.API.Models
{
    public class PedidoTransferencia : Pedido, IAggregateRoot
    {
        public PedidoTransferencia(Guid idLocalDestino, string? nomeLocalDestino, DateTime dataCriacao, Guid idFuncionarioResponsavel, List<PedidoItem> pedidoItens)
            : base(idFuncionarioResponsavel, dataCriacao, pedidoItens)
        {
            IdLocalDestino = idLocalDestino;
            NomeLocalDestino = nomeLocalDestino;
        }

        public Guid IdLocalDestino { get; private set; }
        public string? NomeLocalDestino { get; private set; }

        public PedidoTransferencia() : base() { }
    }
}
