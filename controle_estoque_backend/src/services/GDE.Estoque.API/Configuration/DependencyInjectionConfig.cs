using FluentValidation.Results;
using GDE.Core.Mediator;
using GDE.Core.Usuario;
using GDE.Estoque.API.Application.Commands;
using GDE.Estoque.API.Application.Events;
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
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();


            services.AddScoped<IRequestHandler<AdicionarItensEstoqueCommand, ValidationResult>, EstoqueCommandHandler>();
            services.AddScoped<INotificationHandler<ProdutoMovimentadoEvent>, ProdutoEventHandler>();

            return services;
        }
    }
}
