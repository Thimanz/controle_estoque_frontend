using GDE.Core.Data;
using GDE.Pedidos.API.Models;

namespace GDE.Pedidos.API.Data.Repository
{
    public class PedidoCompraRepository : IPedidoCompraRepository
    {
        private readonly PedidosContext _context;

        public PedidoCompraRepository(PedidosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(PedidoCompra pedidoCompra)
        {
            _context.PedidosCompra.Add(pedidoCompra);
        }

        public void Remover(Guid id)
        {
            _context.Remove(id);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
