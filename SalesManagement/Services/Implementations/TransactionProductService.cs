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
    public class TransactionProductService : IService<TransactionProduct>
    {
        private readonly IGenericRepository<TransactionProduct> _repository;
        private readonly SalesManagementContext _context;
        public TransactionProductService(IGenericRepository<TransactionProduct> repository, SalesManagementContext context)
        {
            _repository = repository;
            _context = context;
        }
        public async Task CreateAsync(TransactionProduct transactionProduct)
        {
            await _repository.AddAsync(transactionProduct);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var item = await _repository.GetByIdAsync(id);
            if (item == null) return;
            _repository.Delete(item);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TransactionProduct>> GetAllAsync() =>await _repository.GetAllAsync(x => x.Include(x => x.Product).Include(x => x.Transaction));

        public async Task<TransactionProduct?> GetByIdAsync(int id) =>await _repository.GetByIdAsync(id, x => x.Include(x => x.Product).Include(x => x.Transaction));

        public async Task UpdateAsync(TransactionProduct transactionProduct)
        {
            _repository.Update(transactionProduct);
            await _context.SaveChangesAsync();
        }
        public async Task<bool> ExistsAsync(int id) => await _repository.ExistsAsync(id);

        public async Task<List<RecentTransactionDto>> GetRecentTransactionsAsync()
        {
            var transactionProducts = await _repository.GetAllAsync();
            return transactionProducts
                     .OrderByDescending(tp => tp.Transaction.DateTime)
                     .Take(10)
                     .Select(tp => new RecentTransactionDto
                     {
                         TransactionId = tp.TransactionId,
                         Type = tp.Transaction.Type == TranactionType.Sell ? "Sell" : "Buy",
                         ProductTitle = tp.Product.Title,
                         Quantity = tp.Quantity,
                         Date = tp.Transaction.DateTime
                     }).ToList();
        }
    }
}
