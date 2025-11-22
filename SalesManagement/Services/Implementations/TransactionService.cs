using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models.DTOs;
using SalesManagement.Models.Entities;
using SalesManagement.Repositories.Interfaces;
using SalesManagement.Services.Interfaces;
using System.Threading.Tasks;

namespace SalesManagement.Services.Implementations
{
    public class TransactionService : IService<Transaction>
    {
        private readonly IGenericRepository<Transaction> _repository;
        private readonly SalesManagementContext _context;
        public TransactionService(IGenericRepository<Transaction> repository, SalesManagementContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task CreateAsync(Transaction transaction)
        {
            await _repository.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return;
            _repository.Delete(item);
            await _context.SaveChangesAsync();
        }


        public async Task<IEnumerable<Transaction>> GetAllAsync() => await _repository.GetAllAsync(x => x.Include(a => a.TransactionProducts).ThenInclude(x => x.Product).Include(x => x.Customer));

        public async Task<Transaction?> GetByIdAsync(int id) => await _repository.GetByIdAsync(id, x => x.Include(a => a.TransactionProducts).ThenInclude(x => x.Product).Include(x => x.Customer));

        public async Task UpdateAsync(Transaction transaction)
        {
            _repository.Update(transaction);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id) => await _repository.ExistsAsync(id);

        public async Task<decimal> GetSalesThisMonthAsync(DateTime startOfMonth)
        {
            var transactions = await GetAllAsync();
            return transactions
                .Where(t => t.DateTime >= startOfMonth && t.Type == TranactionType.Sell)
                  .SelectMany(t => t.TransactionProducts)
                  .Sum(tp => tp.UnitPrice * tp.Quantity);
        }
        public async Task<IEnumerable<SalesByDayDto>> GetSalesByDayAsync()
        {
            var transactions = await GetAllAsync();
            return transactions
                .Where(t => t.DateTime >= DateTime.Today.AddDays(-29) && t.Type == TranactionType.Sell)
                .SelectMany(t => t.TransactionProducts.Select(tp => new SalesByDayDto
                {
                    DateTime = t.DateTime,
                    Quantity = tp.Quantity,
                    UnitPrice = tp.UnitPrice
                }));
        }

        public async Task<IEnumerable<MovementsDto>> GetMovementsAsync()
        {
            var transactions = await GetAllAsync();
            return transactions
                 .Where(t => t.DateTime >= DateTime.Today.AddDays(-6))
                 .SelectMany(t => t.TransactionProducts.Select(tp => new MovementsDto
                 {
                     DateTime = t.DateTime,
                     TranactionType = t.Type,
                     Quantity = tp.Quantity
                 }));
        }

    }
}
