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

        [HttpGet("local")]
        public async Task<Local> ObterLocal(Guid id)
        {
            return await _localRepository.ObterPorId(id);
        }
    }
}
