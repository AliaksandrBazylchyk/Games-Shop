using MongoDB.Bson.Serialization.Attributes;

namespace GamesShop.Catalog.DAL.Entities
{
    public abstract class BaseEntity
    {
        [BsonRequired] public Guid Id { get; set; }
    }
}
