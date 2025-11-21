using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models;

namespace SalesManagement.Controllers
{
    public class TransactionProductsController : Controller
    {
        private readonly SalesManagementContext _context;

        public TransactionProductsController(SalesManagementContext context)
        {
            _context = context;
        }

        // GET: TransactionProducts
        public async Task<IActionResult> Index()
        {
            var salesManagementContext = _context.TransactionProduct.Include(t => t.Product).Include(t => t.Transaction);
            return View(await salesManagementContext.ToListAsync());
        }

        // GET: TransactionProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionProduct = await _context.TransactionProduct
                .Include(t => t.Product)
                .Include(t => t.Transaction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transactionProduct == null)
            {
                return NotFound();
            }

            return View(transactionProduct);
        }

        // GET: TransactionProducts/Create
        public IActionResult Create()
        {
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id");
            ViewData["TransactionId"] = new SelectList(_context.Transaction, "Id", "Id");
            return View();
        }

        // POST: TransactionProducts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductId,TransactionId,Quantity,UnitPrice")] TransactionProduct transactionProduct)
        {
            if (ModelState.IsValid)
            {
                _context.Add(transactionProduct);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", transactionProduct.ProductId);
            ViewData["TransactionId"] = new SelectList(_context.Transaction, "Id", "Id", transactionProduct.TransactionId);
            return View(transactionProduct);
        }

        // GET: TransactionProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionProduct = await _context.TransactionProduct.FindAsync(id);
            if (transactionProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", transactionProduct.ProductId);
            ViewData["TransactionId"] = new SelectList(_context.Transaction, "Id", "Id", transactionProduct.TransactionId);
            return View(transactionProduct);
        }

        // POST: TransactionProducts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductId,TransactionId,Quantity,UnitPrice")] TransactionProduct transactionProduct)
        {
            if (id != transactionProduct.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(transactionProduct);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TransactionProductExists(transactionProduct.Id))
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
            ViewData["ProductId"] = new SelectList(_context.Product, "Id", "Id", transactionProduct.ProductId);
            ViewData["TransactionId"] = new SelectList(_context.Transaction, "Id", "Id", transactionProduct.TransactionId);
            return View(transactionProduct);
        }

        // GET: TransactionProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionProduct = await _context.TransactionProduct
                .Include(t => t.Product)
                .Include(t => t.Transaction)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (transactionProduct == null)
            {
                return NotFound();
            }

            return View(transactionProduct);
        }

        // POST: TransactionProducts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var transactionProduct = await _context.TransactionProduct.FindAsync(id);
            if (transactionProduct != null)
            {
                _context.TransactionProduct.Remove(transactionProduct);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TransactionProductExists(int id)
        {
            return _context.TransactionProduct.Any(e => e.Id == id);
        }
    }
}
