using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models;
using SalesManagement.Models.ViewModels;

namespace SalesManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly SalesManagementContext _context;

        public ProductsController(SalesManagementContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString, string productType, string productCategory)
        {
            var items =
                _context.Product.Include(x => x.Type).ThenInclude(x => x.Category).Select(x =>
                new StockDto
                {
                    Product = x,
                    Quantity = x.TransactionProducts
                    .Sum(x => (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity))
                });
            if (!string.IsNullOrEmpty(searchString))
            {
                items = items.Where(x => x.Product.Title.ToUpper().Contains(searchString.ToUpper())).Select(x =>
                new StockDto
                {
                    Product = x.Product,
                    Quantity = x.Product.TransactionProducts
                    .Sum(x => (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity))
                });
            }

            if (!string.IsNullOrEmpty(productCategory))
            {
                items = items.Where(x => x.Product.Type.CategoryId == int.Parse(productCategory)).Select(x =>
                new StockDto
                {
                    Product = x.Product,
                    Quantity = x.Product.TransactionProducts
                    .Sum(x => (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity))
                });

            }
            if (!string.IsNullOrEmpty(productType))
            {
                items = items.Where(x => x.Product.TypeId == int.Parse(productType)).Select(x =>
                new StockDto
                {
                    Product = x.Product,
                    Quantity = x.Product.TransactionProducts
                    .Sum(x => (x.Transaction.Type == TranactionType.Buy ? x.Quantity : -x.Quantity))
                });
            }
            IEnumerable<ProductType> types = _context.ProductType;
            IEnumerable<Category> categories = _context.Category;
            var vm = new ProductIndexViewModel
            {
                SelectListCategory = new SelectList(categories, "Id", "Title", productCategory),
                SelectListType = new SelectList(types, "Id", "Title", productType),
                Items = items.ToList(),
                SearchString = searchString,
            };
            return View(vm);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(x => x.TransactionProducts)
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.Set<ProductType>(), "Id", "Title");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,TypeId")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.Set<ProductType>(), "Id", "Title", product.TypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.Set<ProductType>(), "Id", "Title", product.TypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,TypeId")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.Set<ProductType>(), "Id", "Title", product.TypeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Product
                .Include(p => p.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _context.Product.FindAsync(id);
            if (product != null)
            {
                _context.Product.Remove(product);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Product.Any(e => e.Id == id);
        }
    }
}
