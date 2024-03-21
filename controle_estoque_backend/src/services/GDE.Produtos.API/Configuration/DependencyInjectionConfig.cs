using GDE.Produtos.API.Data.Repository;
using GDE.Produtos.API.Models;

namespace GDE.Produtos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();

            return services;
        }
    }
}
