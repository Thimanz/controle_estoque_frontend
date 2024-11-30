using GDE.Core.Identidade;
using GDE.Bff.MovimentacaoEstoque.Configuration;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDependencyInjectionConfiguration();
builder.Services.AddApiConfiguration(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddMessageBusConfiguration(builder.Configuration);
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();

app.UseApiConfiguration(app.Environment);

app.Run();
