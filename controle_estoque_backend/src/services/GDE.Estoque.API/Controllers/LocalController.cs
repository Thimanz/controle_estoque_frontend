using GDE.Core.Controllers;
using GDE.Core.Data;
using GDE.Estoque.API.DTO;
using GDE.Estoque.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Estoque.API.Controllers
{
    [Authorize]
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
        public async Task<IEnumerable<LocalDto>> ListaLocais()
        {
            var locais = await _localRepository.ObterTodos();

            return locais.Select(LocalDto.FromEntity);
        }


        [HttpGet("api/estoque/listar-todos")]
        public async Task<PagedResult<LocalDto>> ListaLocaisPaginado([FromQuery] int pageSize = 30, [FromQuery] int pageIndex = 1)
        {
            var (locais, totalResults) = await _localRepository.ObterTodosPaginado(pageSize, pageIndex);

            return new PagedResult<LocalDto>()
            {
                List = locais.Select(LocalDto.FromEntity),
                TotalResults = totalResults,
                TotalPages = ((totalResults + pageSize - 1) / pageSize),
                PageIndex = pageIndex,
                PageSize = pageSize
            };
        }

        [HttpGet("api/estoque/obter-lista-por-produto-id/{produtoId}")]
        public async Task<IEnumerable<Local>> ListaPorProdutoId(Guid produtoId)
        {
            return await _localRepository.ObterListaPorProdutoId(produtoId);
        }

        [HttpPost("api/estoque")]
        public async Task<IActionResult> AdicionarLocal(AdicionarLocalDto local)
        {
            var novoLocal = AdicionarLocalDto.ToEntity(local);

            if(!novoLocal.IsValid())
                return CustomResponse(novoLocal.ValidationResult!);

            _localRepository.Adicionar(AdicionarLocalDto.ToEntity(local));

            await PersistirDados();
            return CustomResponse();
        }

        [HttpPut("api/estoque/{localId}")]
        public async Task<IActionResult> AtualizarLocal(Guid localId, AdicionarLocalDto local)
        {
            var localExistente = await _localRepository.ObterPorId(localId);

            if (localExistente is null)
            {
                AdicionarErroProcessamento("Local não encontrado");
                return CustomResponse();
            }

            localExistente.AlterarNome(local.Nome);
            localExistente.AlterarEndereco(local.Endereco);
            localExistente.AlterarDimensoes(local.Comprimento, local.Largura, local.Altura);

            if (localExistente.EspacoLivreCalculado < 0)
            {
                AdicionarErroProcessamento("Não foi possível alterar as dimensões do local, pois o tamanho dos itens existentes é maior do que as novas medidas.");
                return CustomResponse();
            }

            _localRepository.Atualizar(localExistente);

            await PersistirDados();
            return CustomResponse();
        }

        [HttpDelete("api/estoque/{localId}")]
        public async Task<IActionResult> RemoverLocal(Guid localId)
        {
            var localExistente = await _localRepository.ObterPorId(localId);

            if (localExistente is null)
            {
                AdicionarErroProcessamento("Local não encontrado");
                return CustomResponse();
            }

            _localRepository.Remover(localExistente);

            await PersistirDados();
            return CustomResponse();
        }

        private async Task PersistirDados()
        {
            var commited = await _localRepository.UnitOfWork.Commit();
            if (!commited) AdicionarErroProcessamento("Não foi possível persistir os dados no banco");
        }

    }
}
