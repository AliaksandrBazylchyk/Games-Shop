using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using GamesShop.Common;
using GamesShop.IdentityServer.Data;
using GamesShop.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamesShop.IdentityServer.Extensions
{
    public static class DatabaseExtension
    {
        public static async void InitializeDatabase(this IApplicationBuilder app)
        {
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();

            await serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();

            serviceScope.ServiceProvider.GetRequiredService<IdentityServerDbContext>();

            var context = serviceScope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
            // Migrate Identity Server configurations Database
            await context.Database.MigrateAsync();

            // Seed configurations for identity Server from Config.cs
            if (!await context.Clients.AnyAsync())
            {
                foreach (var client in Config.Clients)
                {
                    await context.Clients.AddAsync(client.ToEntity());
                }

                await context.SaveChangesAsync();
            }
            if (!await context.IdentityResources.AnyAsync())
            {
                foreach (var resource in Config.IdentityResources)
                {
                    await context.IdentityResources.AddAsync(resource.ToEntity());
                }

                await context.SaveChangesAsync();
            }
            if (!await context.ApiScopes.AnyAsync())
            {
                foreach (var resource in Config.ApiScopes)
                {
                    await context.ApiScopes.AddAsync(resource.ToEntity());
                }

                await context.SaveChangesAsync();
            }

            
            var identityContext = serviceScope.ServiceProvider.GetRequiredService<IdentityServerDbContext>();
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();

            // Migrate ASP.NET Identity Database
            await identityContext.Database.MigrateAsync();

            // Seed roles for ASP.NET Identity users
            if (!await identityContext.Roles.AnyAsync())
            {
                foreach (var role in await Role.GetRolesAsync())
                {
                    var roleEntity = new RoleEntity {Id = Guid.NewGuid(), Name = role, NormalizedName = role.ToUpper()};
                    await roleManager.CreateAsync(roleEntity);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}