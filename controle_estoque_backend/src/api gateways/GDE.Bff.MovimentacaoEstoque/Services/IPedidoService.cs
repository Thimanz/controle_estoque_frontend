using GDE.Bff.MovimentacaoEstoque.Models;
using GDE.Core.Communication;

namespace GDE.Bff.MovimentacaoEstoque.Services
{
    public interface IPedidoService
    {
        Task<ResponseResult> AdicionarPedidoCompra(PedidoCompraDTO pedidoDTO);
    }
}
