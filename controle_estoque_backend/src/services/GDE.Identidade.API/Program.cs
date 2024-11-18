using GDE.Identidade.API.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfiguration(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddApiConfiguration();
builder.Services.AddMessageBusConfiguration(builder.Configuration);

var app = builder.Build();

AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

app.UseApiConfiguration(app.Environment);

app.Run();
