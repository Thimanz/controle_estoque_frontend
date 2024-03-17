using GDE.Core.Identidade;
using GDE.Funcionarios.API.Data;
using Microsoft.EntityFrameworkCore;

namespace GDE.Funcionarios.API.Configuration
{
    public static class ApiConfig
    {
        public static void AddApiConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<FuncionariosContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

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

            //if (env.IsDevelopment())
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
