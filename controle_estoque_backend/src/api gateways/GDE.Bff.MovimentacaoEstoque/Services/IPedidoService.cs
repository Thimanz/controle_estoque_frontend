using GDE.Bff.MovimentacaoEstoque.Models;
using GDE.Core.Communication;

namespace GDE.Bff.MovimentacaoEstoque.Services
{
    public interface IPedidoService
    {
        Task<ObterPedidoCompraDTO> ObterPedidoCompra(Guid id);
        Task<ObterPedidoVendaDTO> ObterPedidoVenda(Guid id);
        Task<ObterPedidoTransferenciaDTO> ObterPedidoTransferencia(Guid id);
        Task<ResponseResult> AdicionarPedidoCompra(PedidoCompraDTO pedidoCompraDTO);
        Task<ResponseResult> AdicionarPedidoVenda(PedidoVendaDTO pedidoVendaDTO);
        Task<ResponseResult> AdicionarPedidoTransferencia(PedidoTransferenciaDTO pedidoTransferenciaDTO);
        Task<IEnumerable<ProximosAoVencimentoDTO>> ObterNotificacoesProximoVencimento();
    }
}
