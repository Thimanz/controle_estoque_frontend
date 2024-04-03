//using GDE.Estoque.API.Configuration;
using GDE.Core.Identidade;

var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddDependencyInjectionConfiguration();

//builder.Services.AddApiConfiguration(builder.Configuration);

//builder.Services.AddJwtConfiguration(builder.Configuration);

//builder.Services.AddSwaggerConfiguration();

var app = builder.Build();

//app.UseApiConfiguration(app.Environment);

app.Run();

