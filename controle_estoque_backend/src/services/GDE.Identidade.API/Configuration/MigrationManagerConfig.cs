using GDE.Identidade.API.Data;
using Microsoft.EntityFrameworkCore;

namespace GDE.Identidade.API.Configuration
{
    public static class MigrationManager
    {
        public static IHost MigrateDatabases(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                using (var ctx = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>())
                {
                    ctx.Database.Migrate();
                }
            }
            return host;
        }
    }
}
