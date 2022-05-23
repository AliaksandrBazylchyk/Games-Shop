using System.Reflection;
using GamesShop.IdentityServer.Data;
using GamesShop.IdentityServer.Mapping;
using GamesShop.IdentityServer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GamesShop.IdentityServer.Extensions;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        const string connectionString = "Server=postgres-database;Database=identityDb;Username=postgres;Password=root";
        const string identityConnectionString = "Server=postgres-database;Database=identityUserDb;Username=postgres;Password=root";
        var migrationsAssembly = typeof(Program).GetTypeInfo().Assembly.GetName().Name;

        builder.Services.AddControllers();

        builder.Services.AddDbContext<IdentityServerDbContext>(options =>
            options.UseNpgsql(identityConnectionString));

        builder.Services.AddIdentity<UserEntity, RoleEntity>()
            .AddEntityFrameworkStores<IdentityServerDbContext>()
            .AddDefaultTokenProviders();

        builder.Services.AddAuthorization();

        builder.Services.AddIdentityServer(options =>
            {
                options.EmitStaticAudienceClaim = true;
                /*************************************************************/
                /*               Uncomment this code on Release              */
                /*************************************************************/
                //options.IssuerUri = "http://games-shop.identity-server:5000";
                /*************************************************************/
            })
            .AddInMemoryIdentityResources(Config.IdentityResources)
            .AddInMemoryApiScopes(Config.ApiScopes)
            .AddInMemoryClients(Config.Clients)
            .AddConfigurationStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseNpgsql(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext = builder =>
                    builder.UseNpgsql(connectionString,
                        sql => sql.MigrationsAssembly(migrationsAssembly));
            })
            .AddAspNetIdentity<UserEntity>()
            .AddDeveloperSigningCredential();

        builder.Services.AddAutoMapper(typeof(MappingProfile));

        return builder.Build();
    }

    public static async Task<WebApplication> ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();
        app.InitializeDatabase();
        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseIdentityServer();

        app.UseAuthorization();
                
        app.UseEndpoints(endpoints => endpoints.MapControllers());

        return app;
    }
}