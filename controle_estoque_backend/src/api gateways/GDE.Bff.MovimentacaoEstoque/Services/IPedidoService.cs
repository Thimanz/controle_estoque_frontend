using GDE.Bff.MovimentacaoEstoque.Models;
using GDE.Core.Communication;

namespace GDE.Bff.MovimentacaoEstoque.Services
{
    public interface IPedidoService
    {
        Task<ResponseResult> AdicionarPedidoCompra(PedidoCompraDTO pedidoCompraDTO);
        Task<ResponseResult> AdicionarPedidoVenda(PedidoVendaDTO pedidoVendaDTO);
        Task<ResponseResult> AdicionarPedidoTransferencia(PedidoTransferenciaDTO pedidoTransferenciaDTO);
    }
}
