using GDE.Core.Controllers;
using GDE.Core.Mediator;
using GDE.Funcionarios.API.Application.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GDE.Funcionarios.API.Controllers
{
    [Authorize]
    public class FuncionarioController : MainController
    {
        private readonly IMediatorHandler _mediatorHandler;

        public FuncionarioController(IMediatorHandler mediatorHandler)
        {
            _mediatorHandler = mediatorHandler;
        }

        [HttpGet("funcionarios")]
        public async Task<IActionResult> Index()
        {
            var result = await _mediatorHandler.EnviarComando(
                new RegistrarFuncionarioCommand(Guid.NewGuid(), "Lucas", "44227754801", "lucas@lucas.com"));
                
            return CustomResponse(result);
        }
    }
}
