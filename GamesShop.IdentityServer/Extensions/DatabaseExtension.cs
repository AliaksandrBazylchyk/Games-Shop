using Duende.IdentityServer.EntityFramework.DbContexts;
using Duende.IdentityServer.EntityFramework.Mappers;
using GamesShop.Common;
using GamesShop.IdentityServer.Data;
using GamesShop.IdentityServer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GamesShop.IdentityServer.Extensions
{
    public static class DatabaseExtension
    {
        /// <summary>
        /// Method create> migrate and seed 3 databases for Identity Server and ASP.NET Identity
        /// </summary>
        /// <param name="app">web-application "Identity Server"</param>
        public static async void InitializeDatabase(this IApplicationBuilder app)
        {
            // Getting services factory
            using var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>()!.CreateScope();

            // Getting the Persisted Grants Database context and migrate it
            await serviceScope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.MigrateAsync();

            // Getting the Identity Server Configuration Database context and introduce it for seeds
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

            // Getting the ASP.NET Identity core database context and introduce it for seeds
            var identityContext = serviceScope.ServiceProvider.GetRequiredService<IdentityServerDbContext>();

            // Getting ASP.NET Identity core Role Manager service
            var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<RoleEntity>>();

            // Migrate ASP.NET Identity Database
            await identityContext.Database.MigrateAsync();

            // Seed roles for ASP.NET Identity users
            if (!await identityContext.Roles.AnyAsync())
            {
                foreach (var role in await Role.GetRolesAsync())
                {
                    var roleEntity = new RoleEntity { Id = Guid.NewGuid(), Name = role, NormalizedName = role.ToUpper() };
                    await roleManager.CreateAsync(roleEntity);
                }

                await context.SaveChangesAsync();
            }
        }
    }
}