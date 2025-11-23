using SalesManagement.Models.DTOs;
using SalesManagement.Models.Entities;

namespace SalesManagement.Services.Interfaces
{
    public interface ICategoryService : IService<Category>
    {
        Task<int> GetCountAsync();
        Task<IEnumerable<CategorySharesDto>> GetCategorySharesAsync();
        IEnumerable<Category> Filter(IEnumerable<Category> items, string searchString);
    }
}
