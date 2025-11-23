using Microsoft.AspNetCore.Mvc.Rendering;
using SalesManagement.Models.Entities;

namespace SalesManagement.Models.ViewModels
{
    public class TransactionIndexViewModel
    {
        public List<Transaction> Items { get; set; }
        public string fromDate { get; set; }
        public string toDate { get; set; }
        public SelectList SelectListType { get; set; }
        public SelectList SelectListCustomer { get; set; }
    }
}
