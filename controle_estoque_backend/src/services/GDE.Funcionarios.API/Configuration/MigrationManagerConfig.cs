using GDE.Funcionarios.API.Data;
using Microsoft.EntityFrameworkCore;

namespace GDE.Funcionarios.API.Configuration
{
    public static class MigrationManagerConfig
    {
        public static IHost MigrateDatabases(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetRequiredService<FuncionariosContext>())
                {
                    ctx.Database.Migrate();
                }
            }
            return host;
        }
    }
}
