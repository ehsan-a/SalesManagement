using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models.Entities;
using SalesManagement.Repositories.Interfaces;
using SalesManagement.Services.Interfaces;

namespace SalesManagement.Services.Implementations
{
    public class ProductTypeService : IService<ProductType>
    {
        private readonly IGenericRepository<ProductType> _repository;
        private readonly SalesManagementContext _context;
        public ProductTypeService(IGenericRepository<ProductType> repository, SalesManagementContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task CreateAsync(ProductType productType)
        {
            await _repository.AddAsync(productType);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return;
            _repository.Delete(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<ProductType>> GetAllAsync() => await _repository.GetAllAsync(x => x.Include(x => x.Category).Include(x => x.Products));

        public async Task<ProductType?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id, x => x.Include(x => x.Category).Include(x => x.Products));

        public async Task UpdateAsync(ProductType productType)
        {
            _repository.Update(productType);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id) => await _repository.ExistsAsync(id);

    }
}
