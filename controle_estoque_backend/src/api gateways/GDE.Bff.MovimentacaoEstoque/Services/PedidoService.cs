using GDE.Bff.MovimentacaoEstoque.Extensions;
using GDE.Bff.MovimentacaoEstoque.Models;
using GDE.Core.Communication;
using Microsoft.Extensions.Options;

namespace GDE.Bff.MovimentacaoEstoque.Services
{
    public class PedidoService : Service, IPedidoService
    {
        private readonly HttpClient _httpClient;

        public PedidoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.PedidoUrl);
        }

        public async Task<ObterPedidoCompraDTO> ObterPedidoCompra(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/pedido/compra/{id}");

            if (!TratarErrosResponse(response))
                return null;

            return await DeserializarObjetoResponse<ObterPedidoCompraDTO>(response);
        }

        public async Task<ObterPedidoVendaDTO> ObterPedidoVenda(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/pedido/venda/{id}");

            if (!TratarErrosResponse(response))
                return null;

            return await DeserializarObjetoResponse<ObterPedidoVendaDTO>(response);
        }

        public async Task<ObterPedidoTransferenciaDTO> ObterPedidoTransferencia(Guid id)
        {
            var response = await _httpClient.GetAsync($"api/pedido/transferencia/{id}");

            if (!TratarErrosResponse(response))
                return null;

            return await DeserializarObjetoResponse<ObterPedidoTransferenciaDTO>(response);
        }

        public async Task<ResponseResult> AdicionarPedidoCompra(PedidoCompraDTO pedidoCompraDTO)
        {
            var pedidoContent = ObterConteudo(pedidoCompraDTO);
            var response = await _httpClient.PostAsync("api/pedido/compra", pedidoContent);
            if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);


            return RetornoOk();
        }
        public async Task<ResponseResult> AdicionarPedidoVenda(PedidoVendaDTO pedidoVendaDTO)
        {
            var pedidoContent = ObterConteudo(pedidoVendaDTO);
            try
            {
                var response = await _httpClient.PostAsync("api/pedido/venda", pedidoContent);
                if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return RetornoOk();
        }

        public async Task<ResponseResult> AdicionarPedidoTransferencia(PedidoTransferenciaDTO pedidoTransferenciaDTO)
        {
            var pedidoContent = ObterConteudo(pedidoTransferenciaDTO);
            try
            {
                var response = await _httpClient.PostAsync("api/pedido/transferencia", pedidoContent);
                if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);
            }
            catch (Exception)
            {
                throw;
            }

            return RetornoOk();
        }


        public async Task<IEnumerable<ProximosAoVencimentoDTO>> ObterNotificacoesProximoVencimento()
        {
            var response = await _httpClient.GetAsync($"api/pedido/proximos-ao-vencimento");

            if (!TratarErrosResponse(response))
                return null;

            return await DeserializarObjetoResponse<IEnumerable<ProximosAoVencimentoDTO>>(response);
        }
    }
}
