using MongoDB.Bson.Serialization.Attributes;

namespace GamesShop.Catalog.DAL.Entities
{
    public abstract class BaseEntity
    {
        [BsonId]
        public string Guid { get; set; }
    }
}