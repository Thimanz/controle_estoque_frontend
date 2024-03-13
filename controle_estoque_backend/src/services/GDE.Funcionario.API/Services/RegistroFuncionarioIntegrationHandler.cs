
using EasyNetQ;
using FluentValidation.Results;
using GDE.Core.Mediator;
using GDE.Core.Messages.Integration;
using GDE.Funcionarios.API.Application.Commands;

namespace GDE.Funcionarios.API.Services
{
    public class RegistroFuncionarioIntegrationHandler : BackgroundService
    {
        private IBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroFuncionarioIntegrationHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        } 

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _bus = RabbitHutch.CreateBus("host=localhost:5672");

            _bus.Rpc.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request => 
                new ResponseMessage(await RegistrarFuncionario(request)));

            return Task.CompletedTask;
        }

        private async Task<ValidationResult> RegistrarFuncionario(UsuarioRegistradoIntegrationEvent message)
        {
            var funcionarioCommand = new RegistrarFuncionarioCommand(message.Id, message.Nome, message.Cpf, message.Email);
            ValidationResult result;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediatorHandler>();
                result = await mediator.EnviarComando(funcionarioCommand);
            }

            return result;
        }
    }
}
