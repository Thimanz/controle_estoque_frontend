using GDE.Produtos.API.Repositories;

namespace GDE.Produtos.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IUploadImagemRepository, UploadImageRepository>();

            return services;
        }
    }
}
