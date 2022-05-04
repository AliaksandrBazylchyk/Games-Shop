using GamesShop.Catalog.DAL.Core;
using GamesShop.Common.Configurations;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GamesShop.Catalog.DAL.Contexts
{
    public class CatalogMongoDBContext : ICatalogMongoDBContext
    {
        public IMongoDatabase Db { get; set; }
        public MongoClient MongoClient { get; set; }

        public CatalogMongoDBContext(IOptions<MongoDBConfiguration> configuration)
        {
            MongoClient = new MongoClient(new MongoClientSettings
            {
                Server = new MongoServerAddress(configuration.Value.MongoDBConnectionString, 27017),
            });
            Db = MongoClient.GetDatabase(configuration.Value.MongoDataBaseName);
        }

        public IMongoCollection<T> GetCollectionByName<T>(string name)
        {
            return Db.GetCollection<T>(name);
        }
    }
}