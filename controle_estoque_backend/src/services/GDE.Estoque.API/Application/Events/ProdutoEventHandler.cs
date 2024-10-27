using GDE.Core.Messages.Integration;
using MassTransit;
using MediatR;

namespace GDE.Estoque.API.Application.Events
{
    public class ProdutoEventHandler : INotificationHandler<ProdutoMovimentadoEvent>
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public ProdutoEventHandler(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async Task Handle(ProdutoMovimentadoEvent message, CancellationToken cancellationToken)
        {
            await _publishEndpoint.Publish(new ProdutoMovimentadoIntegrationEvent(message.ProdutoId, message.Quantidade, (TipoMovimentacao)message.Tipo.GetHashCode()));
        }
    }
}
