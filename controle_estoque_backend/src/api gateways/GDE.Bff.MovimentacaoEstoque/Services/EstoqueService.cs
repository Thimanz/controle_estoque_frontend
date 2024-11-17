using GDE.Bff.MovimentacaoEstoque.Extensions;
using GDE.Bff.MovimentacaoEstoque.Models;
using Microsoft.Extensions.Options;

namespace GDE.Bff.MovimentacaoEstoque.Services
{
    public class EstoqueService : Service, IEstoqueService
    {
        private readonly HttpClient _httpClient;

        public EstoqueService(HttpClient httpClient, IOptions<AppServicesSettings> settings)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri(settings.Value.EstoqueUrl);
        }

        public async Task<LocalDTO> ObterLocalPorId(Guid Id)
        {
            var response = await _httpClient.GetAsync($"api/estoque/{Id}");

            if (!TratarErrosResponse(response))
                return null;
            var teste = await response.Content.ReadAsStringAsync();

            return await DeserializarObjetoResponse<LocalDTO>(response);
        }
    }
}
