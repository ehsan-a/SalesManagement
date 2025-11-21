using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models;
using SalesManagement.Models.ViewModels;
using System.Diagnostics;

namespace SalesManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly SalesManagementContext _context;
        public HomeController(ILogger<HomeController> logger, SalesManagementContext context)
        {
            _logger = logger;
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var vm = new DashboardViewModel();

            vm.TotalStockQuantity = _context.Product
                .Include(x => x.TransactionProducts)
                .ThenInclude(x => x.Transaction).ToList()
                .Sum(p => p.TransactionProducts.Sum(x => (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity)));
            vm.CategoriesCount = await _context.Category.CountAsync();
            vm.ActiveProductsCount = await _context.Product.CountAsync(p => p.IsActive);
            var startOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            vm.SalesThisMonth = await _context.Transaction
                .Where(t => t.DateTime >= startOfMonth && t.Type == TranactionType.Sell)
                .SelectMany(t => t.TransactionProducts)
                .SumAsync(tp => tp.UnitPrice * tp.Quantity);


            var last30 = Enumerable.Range(0, 30)
                .Select(i => DateTime.Today.AddDays(-(29 - i))).ToList();
            vm.SalesLast30Labels = last30.Select(d => d.ToString("MM-dd")).ToList();

            var salesByDay = await _context.Transaction
                .Where(t => t.DateTime >= DateTime.Today.AddDays(-29) && t.Type == TranactionType.Sell)
                .SelectMany(t => t.TransactionProducts.Select(tp => new { t.DateTime, tp.Quantity, tp.UnitPrice }))
                .ToListAsync();

            vm.SalesLast30Values = last30
                .Select(day => salesByDay
                    .Where(s => s.DateTime.Date == day.Date)
                    .Sum(s => s.Quantity * s.UnitPrice))
                .Select(v => Math.Round((decimal)v, 2)).ToList();


            var last7 = Enumerable.Range(0, 7).Select(i => DateTime.Today.AddDays(-(6 - i))).ToList();
            vm.MovementLabels = last7.Select(d => d.ToString("MM-dd")).ToList();


            var movements = await _context.Transaction
                .Where(t => t.DateTime >= DateTime.Today.AddDays(-6))
                .SelectMany(t => t.TransactionProducts.Select(tp => new { t.DateTime, t.Type, tp.Quantity }))
                .ToListAsync();

            foreach (var day in last7)
            {
                var buy = movements.Where(m => m.DateTime.Date == day.Date && m.Type == TranactionType.Buy).Sum(m => m.Quantity);
                var sell = movements.Where(m => m.DateTime.Date == day.Date && m.Type == TranactionType.Sell).Sum(m => m.Quantity);
                vm.MovementBuy.Add(buy);
                vm.MovementSell.Add(sell);
            }


            var categoryShares = await _context.Category
                .Select(c => new
                {
                    c.Title,
                    Stock = _context.TransactionProduct.Where(p => p.Product.Type.CategoryId == c.Id).Sum(x =>
                     (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity))
                }).ToListAsync();

            vm.CategoryLabels = categoryShares.Select(c => c.Title).ToList();
            vm.CategoryStockShare = categoryShares.Select(c => c.Stock).ToList();


            vm.LowStockItems = await _context.Product
                .Where(p => p.TransactionProducts.Sum(x => (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity)) <= p.MinQuantity)
                    .Include(p => p.Type.Category)
                    .OrderBy(p => p.TransactionProducts.Sum(x => (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity)))
                    .Select(p => new LowStockItemDto
                    {
                        ProductId = p.Id,
                        ProductTitle = p.Title,
                        CategoryTitle = p.Type.Category.Title,
                        Stock = p.TransactionProducts.Sum(x => (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity)),
                        MinStock = p.MinQuantity
                    }).ToListAsync();


            vm.RecentTransactions = await _context.TransactionProduct
                .Include(tp => tp.Transaction)
                        .Include(tp => tp.Product)
                        .OrderByDescending(tp => tp.Transaction.DateTime)
                        .Take(10)
                        .Select(tp => new RecentTransactionDto
                        {
                            TransactionId = tp.TransactionId,
                            Type = tp.Transaction.Type == TranactionType.Sell ? "Sell" : "Buy",
                            ProductTitle = tp.Product.Title,
                            Quantity = tp.Quantity,
                            Date = tp.Transaction.DateTime
                        }).ToListAsync();

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
