
using FluentValidation.Results;
using GDE.Core.Mediator;
using GDE.Core.Messages.Integration;
using GDE.Funcionarios.API.Application.Commands;
using GDE.MessageBus;

namespace GDE.Funcionarios.API.Services
{
    public class RegistroFuncionarioIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public RegistroFuncionarioIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        private void SetResponder()
        {
            _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(RegistrarFuncionario);
            _bus.AdvancedBus.Connected += OnConnect!;
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetResponder();
            return Task.CompletedTask;
        }

        private void OnConnect(object sender, EventArgs e)
        {
            SetResponder();
        }

        private async Task<ResponseMessage> RegistrarFuncionario(UsuarioRegistradoIntegrationEvent message)
        {
            var funcionarioCommand = new RegistrarFuncionarioCommand(message.Id, message.Nome, message.Cpf, message.Email);
            ValidationResult result;

            using (var scope = _serviceProvider.CreateScope())
            {
                var mediator = scope.ServiceProvider.GetService<IMediatorHandler>();
                result = await mediator.EnviarComando(funcionarioCommand);
            }

            return new ResponseMessage(result);
        }
    }
}
