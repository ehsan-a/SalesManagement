using Microsoft.AspNetCore.Mvc.Rendering;

namespace SalesManagement.Models.ViewModels
{
    public class ProductIndexViewModel
    {
        public List<StockDto> Items { get; set; }
        public string SearchString { get; set; }
        public SelectList SelectListType { get; set; }
        public SelectList SelectListCategory { get; set; }
    }
    public class StockDto
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }
}
