using FluentValidation.Results;
using GDE.Core.Mediator;
using GDE.Estoque.API.Application.Commands;
using GDE.Estoque.Domain;
using GDE.Estoque.Infra.Data.Repository;
using MediatR;

namespace GDE.Estoque.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<ILocalRepository, LocalRepository>();

            services.AddScoped<IRequestHandler<AdicionarItensEstoqueCommand, ValidationResult>, EstoqueCommandHandler>();


            return services;
        }
    }
}
