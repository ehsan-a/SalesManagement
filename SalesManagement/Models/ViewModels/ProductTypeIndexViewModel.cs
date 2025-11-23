using Microsoft.AspNetCore.Mvc.Rendering;
using SalesManagement.Models.Entities;
using System.Net.Sockets;

namespace SalesManagement.Models.ViewModels
{
    public class ProductTypeIndexViewModel
    {
        public List<ProductType> Items { get; set; }
        public string SearchString { get; set; }
        public SelectList SelectListCategory { get; set; }
    }
}
