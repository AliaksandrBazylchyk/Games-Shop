using GamesShop.Catalog.DAL.Entities;

namespace GamesShop.Catalog.BLL.Services.CategoryService
{
    public interface ICategoryService
    {
        Task<Category> GetByIdAsync(string id);
        Task<IEnumerable<Category>> GetAllAsync();
        Task<Category> CreateAsync(string name);
    }
}
