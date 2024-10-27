using GDE.Core.Identidade;
using GDE.Core.MessageBus;
using GDE.Core.Messages.Integration;
using GDE.Funcionarios.API.Consumers;
using MassTransit;

namespace GDE.Funcionarios.API.Configuration
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
                x.AddConsumer<UsuarioRegistradoConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(messageBusCredentials.Host, "/", h =>
                    {
                        h.Username(messageBusCredentials.User);
                        h.Password(messageBusCredentials.Password);
                    });

                    cfg.ReceiveEndpoint(UsuarioRegistradoIntegrationEvent.QueueName, e =>
                    {
                        e.ConfigureConsumer<UsuarioRegistradoConsumer>(context);
                    });
                });
            });

        }
    }
}