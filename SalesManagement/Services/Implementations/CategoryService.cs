using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models.DTOs;
using SalesManagement.Models.Entities;
using SalesManagement.Models.ViewModels;
using SalesManagement.Repositories.Interfaces;
using SalesManagement.Services.Interfaces;
using System.Threading.Tasks;

namespace SalesManagement.Services.Implementations
{
    public class CategoryService : IService<Category>
    {
        private readonly IGenericRepository<Category> _repository;
        private readonly SalesManagementContext _context;
        public CategoryService(IGenericRepository<Category> repository, SalesManagementContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task CreateAsync(Category category)
        {
            await _repository.AddAsync(category);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return;
            _repository.Delete(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllAsync() => await _repository.GetAllAsync(x => x.Include(x => x.ProductTypes));

        public async Task<Category?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id, x => x.Include(x => x.ProductTypes));

        public async Task UpdateAsync(Category category)
        {
            _repository.Update(category);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id) => await _repository.ExistsAsync(id);
        public async Task<int> GetCountAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories.Count();
        }
        public async Task<IEnumerable<CategorySharesDto>> GetCategorySharesAsync()
        {
            var categories = await _repository.GetAllAsync();
            return categories
                .Select(c => new CategorySharesDto
                {
                    Title = c.Title,
                    Stock = _context.TransactionProduct.Where(p => p.Product.Type.CategoryId == c.Id).Sum(x =>
                     (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity))
                });
        }
        public IEnumerable<Category> Filter(IEnumerable<Category> items, string searchString)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(x => x.Title.ToUpper().Contains(searchString.ToUpper()));
            }
            return items;
        }
    }
}
