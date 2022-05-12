using GamesShop.Catalog.DAL.Core;
using GamesShop.Catalog.DAL.Entities;
using MongoDB.Bson;
using MongoDB.Driver;

namespace GamesShop.Catalog.DAL.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        private readonly ICatalogMongoDBContext _context;
        protected IMongoCollection<T> Collection;

        public BaseRepository(
            ICatalogMongoDBContext context
        )
        {
            _context = context;
            Collection = _context.GetCollectionByName<T>(typeof(T).Name);
        }


        public async Task<T> GetByIdAsync(string id)
        {
            //var bsonId = BsonObjectId.Create(id);
            //var filter = Builders<T>.Filter.Eq(doc => doc.Id, bsonId.AsGuid);
            //var query = await Collection.FindAsync(filter);

            //var entity = await query.SingleOrDefaultAsync();
            var query = await Collection.FindAsync(_ => _.Guid == id);
            var entity = await query.FirstOrDefaultAsync();

            return entity;
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = await Collection.FindAsync(_ => true);
            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<T> DeleteAsync(string id)
        {
            var entity = await Collection.FindOneAndDeleteAsync(x => x.Guid == id);

            return entity;
        }

        public async Task<T> UpdateAsync(string id, T entity)
        {
            var result = await Collection.FindOneAndUpdateAsync(
                x => x.Guid == id,
                new ObjectUpdateDefinition<T>(entity)
            );

            return result;
        }

        public async Task<T> CreateAsync(T entity)
        {
            await Collection.InsertOneAsync(entity);

            return entity;
        }
    }
}