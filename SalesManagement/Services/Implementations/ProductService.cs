using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models.Entities;
using SalesManagement.Models.ViewModels;
using SalesManagement.Repositories.Interfaces;
using SalesManagement.Services.Interfaces;
using System.Threading.Tasks;
using SalesManagement.Models.DTOs;

namespace SalesManagement.Services.Implementations
{
    public class ProductService : IService<Product>
    {
        private readonly IGenericRepository<Product> _repository;

        private readonly SalesManagementContext _context;
        public ProductService(IGenericRepository<Product> repository, SalesManagementContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task CreateAsync(Product product)
        {
            await _repository.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return;
            _repository.Delete(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Product>> GetAllAsync() => await _repository.GetAllAsync(x => x.Include(x => x.TransactionProducts).ThenInclude(x => x.Transaction).Include(x => x.Type));

        public async Task<Product?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id, x => x.Include(x => x.TransactionProducts).ThenInclude(x => x.Transaction));

        public async Task UpdateAsync(Product product)
        {
            _repository.Update(product);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id) => await _repository.ExistsAsync(id);
        public async Task<IEnumerable<StockDto>> GetItemsAsync()
        {
            var products = await GetAllAsync();

            return products.Select(p => new StockDto
            {
                Product = p,
                Quantity = p.TransactionProducts?
                    .Sum(tp => (tp.Transaction.Type == TranactionType.Buy ? tp.Quantity : -tp.Quantity)) ?? 0
            });
        }

        public IEnumerable<StockDto> Filter(IEnumerable<StockDto> items, string searchString, string productType, string productCategory)
        {
            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(x => x.Product.Title.ToUpper().Contains(searchString.ToUpper())).Select(p => new StockDto
                {
                    Product = p.Product,
                    Quantity = p.Product.TransactionProducts?
                    .Sum(tp => (tp.Transaction.Type == TranactionType.Buy ? tp.Quantity : -tp.Quantity)) ?? 0
                });
            }
            if (!string.IsNullOrEmpty(productCategory))
            {
                items = items.Where(x => x.Product.Type.CategoryId == int.Parse(productCategory)).Select(p => new StockDto
                {
                    Product = p.Product,
                    Quantity = p.Product.TransactionProducts?
                    .Sum(tp => (tp.Transaction.Type == TranactionType.Buy ? tp.Quantity : -tp.Quantity)) ?? 0
                });

            }
            if (!string.IsNullOrEmpty(productType))
            {
                items = items.Where(x => x.Product.TypeId == int.Parse(productType)).Select(p => new StockDto
                {
                    Product = p.Product,
                    Quantity = p.Product.TransactionProducts?
                    .Sum(tp => (tp.Transaction.Type == TranactionType.Buy ? tp.Quantity : -tp.Quantity)) ?? 0
                });
            }
            return items;
        }
        public async Task<int> GetTotalStockQuantityAsync()
        {
            var products = await GetAllAsync();
            return products.Sum(p => p.TransactionProducts?
                    .Sum(tp => (tp.Transaction.Type == TranactionType.Buy ? tp.Quantity : -tp.Quantity)) ?? 0);
        }
        public async Task<int> GetActiveCountAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Where(x => x.IsActive).Count();
        }

        public async Task<List<LowStockItemDto>> GetLowStockItemsAsync()
        {
            var products = await GetAllAsync();
            return products
      .Where(p => p.TransactionProducts.Sum(x => (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity)) <= p.MinQuantity)
          .OrderBy(p => p.TransactionProducts.Sum(x => (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity)))
          .Select(p => new LowStockItemDto
          {
              ProductId = p.Id,
              ProductTitle = p.Title,
              CategoryTitle = p.Type.Category.Title,
              Stock = p.TransactionProducts.Sum(x => (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity)),
              MinStock = p.MinQuantity
          }).ToList();
        }
    }
}
