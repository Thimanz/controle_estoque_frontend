using GDE.Estoque.API.Data;
using GDE.Estoque.API.Data.Repository;
using GDE.Estoque.API.Models;

namespace GDE.Estoque.API.Configuration
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
