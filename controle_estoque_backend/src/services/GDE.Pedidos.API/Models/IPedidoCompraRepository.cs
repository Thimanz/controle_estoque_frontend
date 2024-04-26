using GDE.Core.Data;

namespace GDE.Pedidos.API.Models
{
    public interface IPedidoCompraRepository : IRepository<PedidoCompra>
    {
        void AdicionarPedidoCompra(PedidoCompra pedidoCompra);
        void RemoverPedidoCompra(Guid id);
    }
}
