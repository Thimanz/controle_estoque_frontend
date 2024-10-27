using GDE.Core.Mediator;
using GDE.Core.Messages.Integration;
using GDE.Funcionarios.API.Application.Commands;
using MassTransit;

namespace GDE.Funcionarios.API.Consumers
{
    public class UsuarioRegistradoConsumer : IConsumer<UsuarioRegistradoIntegrationEvent>
    {

        private readonly IServiceProvider _serviceProvider;

        public UsuarioRegistradoConsumer(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task Consume(ConsumeContext<UsuarioRegistradoIntegrationEvent> context)
        {
            var funcionarioCommand = new RegistrarFuncionarioCommand(context.Message.Id,
                                                                     context.Message.Nome,
                                                                     context.Message.Cpf,
                                                                     context.Message.Email);
            
            using var scope = _serviceProvider.CreateScope();

            var mediator = scope.ServiceProvider.GetService<IMediatorHandler>();
            var result = await mediator.EnviarComando(funcionarioCommand);

            await context.RespondAsync(new ResponseMessage(result));
        }
    }
}