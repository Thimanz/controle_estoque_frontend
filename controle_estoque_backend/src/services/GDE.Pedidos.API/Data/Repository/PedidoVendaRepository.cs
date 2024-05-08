using GDE.Core.Data;
using GDE.Pedidos.API.Models;

namespace GDE.Pedidos.API.Data.Repository
{
    public class PedidoVendaRepository : IPedidoVendaRepository
    {
        private readonly PedidosContext _context;

        public PedidoVendaRepository(PedidosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(PedidoVenda venda)
        {
            _context.PedidosVenda.Add(venda);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
