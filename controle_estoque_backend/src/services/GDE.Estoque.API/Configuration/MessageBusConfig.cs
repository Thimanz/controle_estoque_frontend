using GDE.Core.MessageBus;
using GDE.Core.Messages.Integration;
using GDE.Estoque.API.Consumer;
using MassTransit;

namespace GDE.Estoque.API.Configuration
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
                x.AddConsumer<PedidoCadastradoConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messageBusCredentials.Host, "/", h =>
                    {
                        h.Username(messageBusCredentials.User);
                        h.Password(messageBusCredentials.Host);
                    });

                    cfg.ReceiveEndpoint(PedidoCadastradoIntegrationEvent.QueueName, e =>
                    {
                        e.ConfigureConsumer<PedidoCadastradoConsumer>(context);
                    });
                });
            });
        }
    }
}
