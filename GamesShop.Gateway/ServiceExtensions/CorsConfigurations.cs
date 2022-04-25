namespace GamesShop.Gateway.ServiceExtensions
{
    public static class CorsConfigurations
    {
        public static IServiceCollection AddConfiguredCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowOrigins", builder =>
                {
                    builder
                        .AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });

            return services;
        }
    }
}