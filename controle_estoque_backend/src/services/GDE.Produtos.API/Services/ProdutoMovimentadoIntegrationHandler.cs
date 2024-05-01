using GDE.Core.Messages.Integration;
using GDE.MessageBus;
using GDE.Produtos.API.Data;

namespace GDE.Produtos.API.Services
{
    public class ProdutoMovimentadoIntegrationHandler : BackgroundService
    {
        private readonly IMessageBus _bus;
        private readonly IServiceProvider _serviceProvider;

        public ProdutoMovimentadoIntegrationHandler(IMessageBus bus, IServiceProvider serviceProvider)
        {
            _bus = bus;
            _serviceProvider = serviceProvider;
        }

        private void SetSubscribers()
        {
            _bus.SubscribeAsync<ProdutoMovimentadoIntegrationEvent>("Estoque Alterado", async request =>
                await AlterarQuantidadeEstoque(request));
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            SetSubscribers();
            return Task.CompletedTask;
        }

        private async Task AlterarQuantidadeEstoque(ProdutoMovimentadoIntegrationEvent message)
        {
            using var scope = _serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ProdutoContext>();

            var produto = await context.Produtos.FindAsync(message.ProdutoId);

            if (produto is not null)
            {
                produto.AdicionarEstoque(message.Quantidade);
                context.Produtos.Update(produto);
                await context.Commit();
            }
        }
    }
}
