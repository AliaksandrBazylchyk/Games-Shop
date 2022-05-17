using Duende.IdentityServer.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace GamesShop.IdentityServer.Extensions
{
    public static class MigrationManager
    {
        public static async Task<IApplicationBuilder> MigrateDatabase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            await scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.EnsureCreatedAsync();
            await scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();
            await using var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            try
            {
                await context.Database.EnsureCreatedAsync();
                await context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {
                throw;
            }

            return app;
        }
    }
}