using SalesManagement.Models.DTOs;
using SalesManagement.Models.Entities;

namespace SalesManagement.Services.Interfaces
{
    public interface ITransactionProductService : IService<TransactionProduct>
    {
        Task<List<RecentTransactionDto>> GetRecentTransactionsAsync();
    }
}
