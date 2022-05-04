using MongoDB.Driver;

namespace GamesShop.Catalog.DAL.Core
{
    public interface IBaseMongoDBContext
    {
        IMongoCollection<T> GetCollectionByName<T>(string name);
    }
}