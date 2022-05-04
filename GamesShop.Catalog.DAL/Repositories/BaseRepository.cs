using GamesShop.Catalog.DAL.Core;
using GamesShop.Catalog.DAL.Entities;
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
            Collection = _context.GetCollectionByName<T>(nameof(T));
        }

        public async Task<T> GetByIdAsync(Guid id)
        {
            var entity = await Collection.FindAsync(x => x.Id == id);

            return await entity.FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            var query = await Collection.FindAsync(_ => true);
            var entities = await query.ToListAsync();

            return entities;
        }

        public async Task<T> DeleteAsync(Guid id)
        {
            var entity = await Collection.FindOneAndDeleteAsync(x => x.Id == id);

            return entity;
        }

        public async Task<T> UpdateAsync(Guid id, T entity)
        {
            var result = await Collection.FindOneAndUpdateAsync(
                x => x.Id == id, 
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