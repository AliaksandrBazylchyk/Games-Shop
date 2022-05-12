using MongoDB.Bson.Serialization.Attributes;

namespace GamesShop.Catalog.DAL.Entities
{
    public class Category : BaseEntity
    {
        [BsonRequired] public string? Name { get; set; }
    }
}