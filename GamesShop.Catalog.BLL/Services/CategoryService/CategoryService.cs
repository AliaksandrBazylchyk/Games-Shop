using GamesShop.Catalog.DAL.Entities;
using GamesShop.Catalog.DAL.Repositories.CategoryRepository;

namespace GamesShop.Catalog.BLL.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(
            ICategoryRepository categoryRepository
        )
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Category> GetByIdAsync(string id)
        {
            var entity = await _categoryRepository.GetByIdAsync(id);

            return entity;
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _categoryRepository.GetAllAsync();
        }

        public async Task<Category> CreateAsync(string name)
        {
            var category = new Category
            {
                Guid = Guid.NewGuid().ToString(), 
                Name = name
            };

            return await _categoryRepository.CreateAsync(category);
        }
    }
}