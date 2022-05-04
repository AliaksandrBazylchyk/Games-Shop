namespace GamesShop.Catalog.DAL.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> DeleteAsync(Guid id);
        Task<T> UpdateAsync(Guid id, T entity);
        Task<T> CreateAsync(T entity);
    }
}