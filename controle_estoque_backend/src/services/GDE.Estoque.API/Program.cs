using GDE.Core.Identidade;
using GDE.Estoque.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDependencyInjectionConfiguration();
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining(typeof(Program)));
builder.Services.AddMessageBusConfiguration(builder.Configuration);

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.MapGet("/health", () => Results.Ok("Healthy"));

app.UseApiConfiguration(app.Environment);

app.Run();

