using GDE.Core.Data;
using GDE.Pedidos.API.Models;

namespace GDE.Pedidos.API.Data.Repository
{
    public class PedidoTransferenciaRepository : IPedidoTransferenciaRepository
    {
        private readonly PedidosContext _context;

        public PedidoTransferenciaRepository(PedidosContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Adicionar(PedidoTransferencia transferencia)
        {
            var a = _context.Add(transferencia);
        }

        public void Dispose()
        {
            _context?.Dispose();
        }
    }
}
