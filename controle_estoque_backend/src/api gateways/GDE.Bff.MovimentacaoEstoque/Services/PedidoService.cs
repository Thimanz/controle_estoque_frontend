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

        public async Task<ResponseResult> AdicionarPedidoCompra(PedidoCompraDTO pedidoDTO)
        {
            var pedidoContent = ObterConteudo(pedidoDTO);
            try
            {
                var response = await _httpClient.PostAsync("api/pedido/compra", pedidoContent);
                if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);
            }
            catch (Exception)
            {

                throw;
            }

            
            return RetornoOk();
        }
    }
}
