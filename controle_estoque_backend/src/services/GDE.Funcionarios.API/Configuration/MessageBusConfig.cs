using GDE.Core.Utils;
using GDE.Funcionarios.API.Services;
using GDE.MessageBus;

namespace GDE.Funcionarios.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegistroFuncionarioIntegrationHandler>();
        }
    }
}