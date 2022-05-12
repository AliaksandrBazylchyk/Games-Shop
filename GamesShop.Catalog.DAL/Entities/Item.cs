using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;

namespace GamesShop.Catalog.DAL.Entities
{
    [Serializable]
    public class Item : BaseEntity
    {
        [BsonRequired] public Guid CategoryId { get; set; }
        [BsonRequired] public string Name { get; set; }
        [BsonRequired] public double Price { get; set; }
        [BsonRequired] public string Description { get; set; }
        [BsonRequired] public string Developer { get; set; }
    }
}