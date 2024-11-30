using GDE.Core.Data;

namespace GDE.Pedidos.API.Models
{
    public interface IPedidoCompraRepository : IRepository<PedidoCompra>
    {
        void Adicionar(PedidoCompra pedidoCompra);
        void Remover(Guid id);
    }
}
