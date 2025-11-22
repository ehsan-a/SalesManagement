using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models.Entities;
using SalesManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Controllers
{
    public class TransactionProductsController : Controller
    {
        private readonly IService<TransactionProduct> _service;
        private readonly IService<Transaction> _transactionService;
        private readonly IService<Product> _productService;

        public TransactionProductsController(IService<TransactionProduct> service, IService<Transaction> transactionService, IService<Product> productService)
        {
            _service = service;
            _transactionService = transactionService;
            _productService = productService;
        }

        // GET: TransactionProducts
        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        // GET: TransactionProducts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionProduct = await _service.GetByIdAsync(id.Value);
            if (transactionProduct == null)
            {
                return NotFound();
            }

            return View(transactionProduct);
        }

        // GET: TransactionProducts/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ProductId"] = new SelectList(await _productService.GetAllAsync(), "Id", "Id");
            ViewData["TransactionId"] = new SelectList(await _transactionService.GetAllAsync(), "Id", "Id");
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
                await _service.CreateAsync(transactionProduct);
                return RedirectToAction(nameof(Index));
            }
            ViewData["ProductId"] = new SelectList(await _productService.GetAllAsync(), "Id", "Id", transactionProduct.ProductId);
            ViewData["TransactionId"] = new SelectList(await _transactionService.GetAllAsync(), "Id", "Id", transactionProduct.TransactionId);
            return View(transactionProduct);
        }

        // GET: TransactionProducts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionProduct = await _service.GetByIdAsync(id.Value);
            if (transactionProduct == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(await _productService.GetAllAsync(), "Id", "Id", transactionProduct.ProductId);
            ViewData["TransactionId"] = new SelectList(await _transactionService.GetAllAsync(), "Id", "Id", transactionProduct.TransactionId);
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
                    await _service.UpdateAsync(transactionProduct);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await TransactionProductExists(transactionProduct.Id) == false)
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
            ViewData["ProductId"] = new SelectList(await _productService.GetAllAsync(), "Id", "Id", transactionProduct.ProductId);
            ViewData["TransactionId"] = new SelectList(await _transactionService.GetAllAsync(), "Id", "Id", transactionProduct.TransactionId);
            return View(transactionProduct);
        }

        // GET: TransactionProducts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var transactionProduct = await _service.GetByIdAsync(id.Value);
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
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> TransactionProductExists(int id)
        {
            return await _service.ExistsAsync(id);
        }
    }
}
