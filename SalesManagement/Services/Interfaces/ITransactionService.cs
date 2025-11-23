using SalesManagement.Models.DTOs;
using SalesManagement.Models.Entities;

namespace SalesManagement.Services.Interfaces
{
    public interface ITransactionService : IService<Transaction>
    {
        Task<decimal> GetSalesThisMonthAsync(DateTime startOfMonth);
        Task<IEnumerable<SalesByDayDto>> GetSalesByDayAsync();
        Task<IEnumerable<MovementsDto>> GetMovementsAsync();
        IEnumerable<Transaction> Filter(IEnumerable<Transaction> items, string transactionType, string transactionCustomer, string fromDate, string toDate);
    }
}
