using GamesShop.Common.Configurations;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace GamesShop.Catalog.API.ServiceExtensions
{
    public static class ConfigurationLoader
    {
        public static IServiceCollection LoadConfigurations(this IServiceCollection services)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddEnvironmentVariables()
                .Build();

            services.Configure<MongoDBConfiguration>(options =>
            {
                options.MongoDBConnectionString = configuration.GetValue<string>("mongoDBConnectionString");
                options.MongoDataBaseName = configuration.GetValue<string>("mongoDataBaseName");
            });

            return services;
        }
    }
}