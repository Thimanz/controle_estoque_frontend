using FluentValidation.Results;
using GDE.Core.DomainObjects;

namespace GDE.Pedidos.API.Models
{
    public class PedidoCompra : Pedido, IAggregateRoot
    {
        public PedidoCompra(string? nomeFornecedor, Guid idFuncionarioResponsavel, List<PedidoItem> pedidoItens)
            : base(idFuncionarioResponsavel, pedidoItens)
        {
            NomeFornecedor = nomeFornecedor;
        }
        public PedidoCompra() : base() { }

        public string? NomeFornecedor { get; private set; }

    }
}
