using GDE.Core.Data;

namespace GDE.Pedidos.API.Models
{
    public interface IPedidoTransferenciaRepository : IRepository<PedidoTransferencia>
    {
        void Adicionar(PedidoTransferencia transferencia);
    }
}
