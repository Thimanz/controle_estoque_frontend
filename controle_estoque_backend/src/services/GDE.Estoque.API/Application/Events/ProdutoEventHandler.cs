using GDE.Core.Messages.Integration;
using GDE.MessageBus;
using MediatR;

namespace GDE.Estoque.API.Application.Events
{
    public class ProdutoEventHandler : INotificationHandler<ProdutoMovimentadoEvent>
    {
        private readonly IMessageBus _bus;

        public ProdutoEventHandler(IMessageBus bus)
        {
            _bus = bus;
        }

        public async Task Handle(ProdutoMovimentadoEvent message, CancellationToken cancellationToken)
        {
            await _bus.PublishAsync(new ProdutoMovimentadoIntegrationEvent(message.ProdutoId, message.Quantidade));
        }
    }
}
