using Microsoft.Extensions.DependencyInjection;

namespace GDE.MessageBus
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMessageBus(this IServiceCollection services, string connectionString)
        {
            if(connectionString == null) throw new ArgumentNullException(nameof(connectionString));

            services.AddSingleton<IMessageBus>(new MessageBus(connectionString));

            return services;
        }
    }
}
