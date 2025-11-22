using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models.Entities;
using SalesManagement.Models.ViewModels;
using SalesManagement.Services.Implementations;
using SalesManagement.Services.Interfaces;
using System.Diagnostics;

namespace SalesManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly TransactionService _transactionService;
        private readonly TransactionProductService _transactionProductService;
        public HomeController(ILogger<HomeController> logger, IService<Product> productService, IService<Category> categoryService, IService<Transaction> transactionService, IService<TransactionProduct> transactionProductService)
        {
            _logger = logger;
            _productService = (ProductService)productService;
            _categoryService = (CategoryService)categoryService;
            _transactionService = (TransactionService)transactionService;
            _transactionProductService = (TransactionProductService)transactionProductService;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel();

            vm.TotalStockQuantity = await _productService.GetTotalStockQuantityAsync();
            vm.CategoriesCount = await _categoryService.GetCountAsync();
            vm.ActiveProductsCount = await _productService.GetActiveCountAsync();
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            vm.SalesThisMonth = await _transactionService.GetSalesThisMonthAsync(startOfMonth);
            var last30 = Enumerable.Range(0, 30)
                .Select(i => DateTime.Today.AddDays(-(29 - i))).ToList();

            vm.SalesLast30Labels = last30.Select(d => d.ToString("MM-dd")).ToList();
            var salesByDay = await _transactionService.GetSalesByDayAsync();

            vm.SalesLast30Values = last30
                .Select(day => salesByDay
                    .Where(s => s.DateTime.Date == day.Date)
                    .Sum(s => s.Quantity * s.UnitPrice))
                .Select(v => Math.Round((decimal)v, 2)).ToList();

            var last7 = Enumerable.Range(0, 7).Select(i => DateTime.Today.AddDays(-(6 - i))).ToList();
            vm.MovementLabels = last7.Select(d => d.ToString("MM-dd")).ToList();
            var movements = await _transactionService.GetMovementsAsync();
            foreach (var day in last7)
            {
                var buy = movements.Where(m => m.DateTime.Date == day.Date && m.TranactionType == TranactionType.Buy).Sum(m => m.Quantity);
                var sell = movements.Where(m => m.DateTime.Date == day.Date && m.TranactionType == TranactionType.Sell).Sum(m => m.Quantity);
                vm.MovementBuy.Add(buy);
                vm.MovementSell.Add(sell);
            }
            var categoryShares = await _categoryService.GetCategorySharesAsync();
            vm.CategoryLabels = categoryShares.Select(c => c.Title).ToList();
            vm.CategoryStockShare = categoryShares.Select(c => c.Stock).ToList();
            vm.LowStockItems = await _productService.GetLowStockItemsAsync();
            vm.RecentTransactions = await _transactionProductService.GetRecentTransactionsAsync();

            return View(vm);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
