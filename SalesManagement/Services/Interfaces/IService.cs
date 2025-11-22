using SalesManagement.Models;

namespace SalesManagement.Services.Interfaces
{
    public interface IService<T>
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(int id);
        Task CreateAsync(T customer);
        Task UpdateAsync(T customer);
        Task DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}
