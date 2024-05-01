using GDE.Produtos.API.Data;

namespace GDE.Produtos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<ProdutoContext>();

            return services;
        }
    }
}
