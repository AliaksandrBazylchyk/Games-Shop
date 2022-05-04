using MongoDB.Bson.Serialization.Attributes;

namespace GamesShop.Catalog.DAL.Entities
{
    public class Item : BaseEntity
    {
        [BsonRequired] public Guid CategoryId { get; set; }
        [BsonRequired] public string Name { get; set; }
        [BsonRequired] public double Price { get; set; }
        [BsonRequired] public string Description { get; set; }
        [BsonRequired] public string Developer { get; set; }
    }
}