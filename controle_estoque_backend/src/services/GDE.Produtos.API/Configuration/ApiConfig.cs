using GDE.Core.Data;
using GDE.Core.Identidade;
using GDE.Produtos.API.Data;
using Microsoft.EntityFrameworkCore;

namespace GDE.Produtos.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ProdutoContext>(options =>
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection")));

            services.AddControllers();

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
            app.EnsureMigrationOfContext<ProdutoContext>();

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
