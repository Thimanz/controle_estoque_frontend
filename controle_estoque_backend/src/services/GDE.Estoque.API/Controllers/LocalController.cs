using GDE.Core.Controllers;
using GDE.Estoque.Domain;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Estoque.API.Controllers
{
    public class LocalController : MainController
    {
        private readonly ILocalRepository _localRepository;

        public LocalController(ILocalRepository localRepository)
        {
            _localRepository = localRepository;
        }

        [HttpGet("api/estoque/{id}")]
        public async Task<Local> ObterLocal(Guid id)
        {
            return await _localRepository.ObterPorId(id);
        }

        [HttpGet("api/estoque")]
        public async Task<IEnumerable<Local>> ListaLocais()
        {
            return await _localRepository.ObterTodos();
        }

        [HttpGet("api/estoque/obter-lista-por-produto-id/{produtoId}")]
        public async Task<IEnumerable<Local>> ListaPorProdutoId(Guid produtoId)
        {
            return await _localRepository.ObterListaPorProdutoId(produtoId);
        }
    }
}
