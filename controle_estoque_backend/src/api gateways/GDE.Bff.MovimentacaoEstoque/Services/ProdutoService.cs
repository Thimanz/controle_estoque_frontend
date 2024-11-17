using GDE.Bff.MovimentacaoEstoque.Extensions;
using GDE.Bff.MovimentacaoEstoque.Models;
using Microsoft.Extensions.Options;

namespace GDE.Bff.MovimentacaoEstoque.Services
{
    public class ProdutoService : Service, IProdutoService
    {
        private readonly HttpClient _httpClient;

        public ProdutoService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.ProdutoUrl);
        }
        public async Task<ProdutoDTO> ObterProdutoPorId(Guid Id)
        {
            var response = await _httpClient.GetAsync($"api/produto/{Id}");

            if (!TratarErrosResponse(response))
                return null;

            return await DeserializarObjetoResponse<ProdutoDTO>(response);
        }

        public async Task<IEnumerable<NotificacaoDTO>> ObterNotificacoesEstoqueBaixo()
        {
            try
            {
                var response = await _httpClient.GetAsync($"api/produto/nivel-estoque-baixo");
                if (!TratarErrosResponse(response))
                    return null;

                return await DeserializarObjetoResponse<IEnumerable<NotificacaoDTO>>(response);
            }
            catch (Exception wx)
            {

                throw;
            }


        }
    }
}
