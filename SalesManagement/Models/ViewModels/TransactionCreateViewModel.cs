using SalesManagement.Models.Entities;

namespace SalesManagement.Models.ViewModels
{
    public class TransactionCreateViewModel
    {
        public TranactionType Type { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;
        public int CustomerId { get; set; }
        public List<TransactionProductViewModel> Items { get; set; } = new();
    }
}
