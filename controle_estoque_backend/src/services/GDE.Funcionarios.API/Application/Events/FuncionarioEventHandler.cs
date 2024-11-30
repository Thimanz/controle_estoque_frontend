using MediatR;

namespace GDE.Funcionarios.API.Application.Events
{
    public class FuncionarioEventHandler : INotificationHandler<FuncionarioRegistradoEvent>
    {
        public Task Handle(FuncionarioRegistradoEvent notification, CancellationToken cancellationToken)
        {
            //TODO: Enviar evento de confirmação de cadastro por email
            return Task.CompletedTask;
        }
    }
}
