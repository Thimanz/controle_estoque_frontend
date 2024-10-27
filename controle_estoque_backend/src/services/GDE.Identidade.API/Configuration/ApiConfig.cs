using GDE.Core.Data;
using GDE.Core.Identidade;
using GDE.Identidade.API.Data;

namespace GDE.Identidade.API.Configuration
{
    public static class ApiConfig
    {
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services)
        {
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

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.EnsureMigrationOfContext<ApplicationDbContext>();

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

            return app;
        }
    }
}
