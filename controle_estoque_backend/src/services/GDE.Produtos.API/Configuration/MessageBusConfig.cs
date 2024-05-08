using GDE.Core.Utils;
using GDE.MessageBus;
using GDE.Produtos.API.Services;

namespace GDE.Produtos.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<ProdutoMovimentadoIntegrationHandler>();
        }
    }
}