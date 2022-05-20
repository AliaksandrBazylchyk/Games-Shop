using Microsoft.EntityFrameworkCore;
using Serilog;

namespace GamesShop.IdentityServer.Extensions;

internal static class HostingExtensions
{
    public static WebApplication ConfigureServices(this WebApplicationBuilder builder)
    {

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
            .AddDeveloperSigningCredential();

        return builder.Build();
    }

    public static async Task<WebApplication> ConfigurePipeline(this WebApplication app)
    {
        app.UseSerilogRequestLogging();

        if (app.Environment.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseRouting();
        app.UseIdentityServer();

        return app;
    }
}