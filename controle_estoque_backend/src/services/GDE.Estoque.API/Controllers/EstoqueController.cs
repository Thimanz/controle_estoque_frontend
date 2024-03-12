using GDE.Core.Controllers;
using GDE.Core.Identidade;
using GDE.Estoque.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Estoque.API.Controllers
{
    [Authorize]
    [Route("api/estoque")]
    public class EstoqueController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;

        public EstoqueController(IProdutoRepository produtoRepository)
        {
            _produtoRepository = produtoRepository;
        }

        [AllowAnonymous]
        [HttpGet("/produtos")]
        public async Task<IEnumerable<Produto>> ObterTodos()
        {
            return await _produtoRepository.ObterTodos();
        }

        [ClaimsAuthorize("Estoque", "Ler")]
        [HttpGet("/produtos/{id}")]
        public async Task<Produto> ProdutoDetalhe(Guid id)
        {
            return await _produtoRepository.ObterPorId(id);
        }
    }
}
