using System.Text.Json.Serialization;
using GDE.Core.Data;
using GDE.Core.Identidade;
using GDE.Estoque.Infra.Data;
using Microsoft.EntityFrameworkCore;

namespace GDE.Estoque.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EstoqueContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

            services.AddCors(options =>
            {
                options.AddPolicy("Total",
                    builder =>
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader());
            });

            services.AddEndpointsApiExplorer();
        }

        public static void UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.EnsureMigrationOfContext<EstoqueContext>();
            
            //if (env.IsDevelopment() || env.IsEnvironment("Local"))
            //{
            app.UseDeveloperExceptionPage();
                app.UseSwaggerConfiguration();
            //}

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("Total");

            app.UseAuthConfiguration();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
