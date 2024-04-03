using GDE.Core.Identidade;
using GDE.Produtos.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencyInjectionConfiguration();
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));
builder.Services.AddMessageBusConfiguration(builder.Configuration);

var app = builder.Build();

app.UseApiConfiguration(app.Environment);
app.Run();
