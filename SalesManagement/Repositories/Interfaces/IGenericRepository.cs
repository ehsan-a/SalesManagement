using System.Linq.Expressions;

namespace SalesManagement.Repositories.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(int id, params Func<IQueryable<T>, IQueryable<T>>[] includeChains);
        Task<IEnumerable<T>> GetAllAsync(params Func<IQueryable<T>, IQueryable<T>>[] includeChains);
        Task AddAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task<bool> ExistsAsync(int id);
    }
}
