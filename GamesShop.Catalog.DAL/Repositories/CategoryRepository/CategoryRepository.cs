using GamesShop.Catalog.DAL.Core;
using GamesShop.Catalog.DAL.Entities;

namespace GamesShop.Catalog.DAL.Repositories.CategoryRepository;

public class CategoryRepository : BaseRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ICatalogMongoDBContext context) : base(context)
    { }
}