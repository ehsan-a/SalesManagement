using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models;
using SalesManagement.Models.Entities;
using SalesManagement.Models.ViewModels;
using SalesManagement.Services.Implementations;
using SalesManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SalesManagement.Controllers
{
    public class TransactionsController : Controller
    {
        private readonly SalesManagementContext _context;
        private readonly TransactionService _service;
        private readonly IService<Product> _productService;
        private readonly IService<Customer> _customerService;

        public TransactionsController(SalesManagementContext context, IService<Transaction> service, IService<Product> productService, IService<Customer> customerService)
        {
            _context = context;
            _service = (TransactionService)service;
            _productService = productService;
            _customerService = customerService;

        }

        // GET: Transactions
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: Transactions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _service.GetByIdAsync(id.Value);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // GET: Transactions/Create
        public async Task<IActionResult> Create()
        {
            var vm = new TransactionCreateViewModel();
            ViewBag.Products = (await _productService.GetAllAsync()).ToList();
            ViewBag.Customers = (await _customerService.GetAllAsync()).ToList();
            return View(vm);
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TransactionCreateViewModel model)
        {
            var transaction = new Transaction
            {
                Type = model.Type,
                DateTime = model.DateTime,
                CustomerId = model.CustomerId,
                TransactionProducts = model.Items.Select(i => new TransactionProduct
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                }).ToList()
            };

            await _service.CreateAsync(transaction);
            return RedirectToAction(nameof(Index));
        }

        // GET: Transactions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _service.GetByIdAsync(id.Value);
            if (transaction == null)
            {
                return NotFound();
            }
            ViewBag.Products = (await _productService.GetAllAsync()).ToList();
            ViewBag.Customers = (await _customerService.GetAllAsync()).ToList();
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Transaction transaction)
        {
            if (id != transaction.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    foreach (var item in transaction.TransactionProducts)
                    {
                        if (item.ProductId == 0)
                        {
                            _context.TransactionProduct.Remove(item);
                        }
                    }
                    _context.Update(transaction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await TransactionExists(transaction.Id) == false)
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
            ViewBag.Products = (await _productService.GetAllAsync()).ToList();
            ViewBag.Customers = (await _customerService.GetAllAsync()).ToList();
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transaction = await _service.GetByIdAsync(id.Value);
            if (transaction == null)
            {
                return NotFound();
            }

            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TransactionExists(int id)
        {
            return await _service.ExistsAsync(id);
        }
    }
}
