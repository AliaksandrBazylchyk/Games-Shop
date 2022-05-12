namespace GamesShop.Catalog.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> DeleteAsync(string id);
        Task<T> UpdateAsync(string id, T entity);
        Task<T> CreateAsync(T entity);
    }
}