using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GamesShop.IdentityServer.Extensions;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {
        var migrationsAssembly = typeof(Program).Assembly.GetName().Name;

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
                options.ConfigureDbContext =
                    builder => builder.UseNpgsql(
                        "Host=postgres;Port=5432;Database=identity;Username=postgres;Password=root",
                        opt => opt.MigrationsAssembly(migrationsAssembly));
            })
            .AddOperationalStore(options =>
            {
                options.ConfigureDbContext =
                    builder => builder.UseNpgsql(
                        "Host=postgres;Port=5432;Database=identity;Username=postgres;Password=root",
                        opt => opt.MigrationsAssembly(migrationsAssembly));
            })
            .AddDeveloperSigningCredential();

        return builder.Build();
    }

    public static async Task<WebApplication> ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        await app.MigrateDatabase();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseIdentityServer();

        return app;
    }
}