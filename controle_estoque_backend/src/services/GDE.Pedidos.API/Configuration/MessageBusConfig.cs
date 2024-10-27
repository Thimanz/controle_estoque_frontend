using GDE.Core.MessageBus;
using GDE.Core.Utils;
using MassTransit;

namespace GDE.Pedidos.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services, IConfiguration configuration)
        {

            var messageBusCredentialsSection = configuration.GetSection("MessageBusConfig");
            services.Configure<MessageBusCredentials>(messageBusCredentialsSection);

            var messageBusCredentials = messageBusCredentialsSection.Get<MessageBusCredentials>();

            if (messageBusCredentials == null) throw new ArgumentNullException(nameof(messageBusCredentials));

            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messageBusCredentials.Host, "/", h =>
                    {
                        h.Username(messageBusCredentials.User);
                        h.Password(messageBusCredentials.Password);
                    });
                });
            });
        }
    }
}