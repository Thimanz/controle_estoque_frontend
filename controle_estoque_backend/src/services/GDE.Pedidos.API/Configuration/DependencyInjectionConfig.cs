using GDE.Core.Mediator;

namespace GDE.Pedidos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            
            
            return services;
        }
    }
}
