using GDE.Core.Data;

namespace GDE.Pedidos.API.Models
{
    public interface IPedidoVendaRepository : IRepository<PedidoVenda>
    {
        void Adicionar(PedidoVenda venda);
    }
}
