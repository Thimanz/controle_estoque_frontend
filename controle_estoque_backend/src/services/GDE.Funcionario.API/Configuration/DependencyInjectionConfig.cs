using FluentValidation.Results;
using GDE.Core.Mediator;
using GDE.Funcionarios.API.Application.Commands;
using GDE.Funcionarios.API.Application.Events;
using GDE.Funcionarios.API.Data.Repository;
using GDE.Funcionarios.API.Models;
using GDE.Funcionarios.API.Services;
using MediatR;

namespace GDE.Funcionarios.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            services.AddScoped<IRequestHandler<RegistrarFuncionarioCommand, ValidationResult>, FuncionarioCommandHandler>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();

            services.AddScoped<INotificationHandler<FuncionarioRegistradoEvent>, FuncionarioEventHandler>();

            services.AddHostedService<RegistroFuncionarioIntegrationHandler>();

            return services;
        }
    }
}
