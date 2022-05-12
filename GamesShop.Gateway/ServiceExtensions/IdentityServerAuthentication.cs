using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace GamesShop.Gateway.ServiceExtensions
{
    public static class IdentityServerAuthentication
    {
        public static IServiceCollection AddIdentityServerAuthentication(
            this IServiceCollection services,
            string identityServerConnectionString
        )
        {
            services.AddAuthentication(s =>
            {
                s.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                s.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Authority = identityServerConnectionString;
                /*********************************************************/
                /*          TODO Comment this code on Release            */
                /*********************************************************/
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters.ValidateAudience = false;
                options.TokenValidationParameters.ValidateIssuer = false;
                /*********************************************************/
            });

            return services;
        }
    }
}