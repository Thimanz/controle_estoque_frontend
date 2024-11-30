using GDE.Core.Utils;
using GDE.Estoque.API.Services;
using GDE.MessageBus;

namespace GDE.Estoque.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<PedidoCadastradoIntegrationHandler>();
        }
    }
}
