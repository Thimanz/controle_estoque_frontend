using GDE.Core.Identidade;
using GDE.Funcionarios.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencyInjectionConfiguration();

builder.Services.AddApiConfiguration(builder.Configuration);

builder.Services.AddJwtConfiguration(builder.Configuration);

builder.Services.AddSwaggerConfiguration();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));

var app = builder.Build();

app.UseApiConfiguration(app.Environment);

app.Run();