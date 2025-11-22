using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SalesManagement.Data;
using SalesManagement.Models.Entities;
using SalesManagement.Models.ViewModels;
using SalesManagement.Services.Implementations;
using SalesManagement.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesManagement.Controllers
{
    public class ProductsController : Controller
    {
        private readonly ProductService _service;
        private readonly IService<Category> _categoryService;
        private readonly IService<ProductType> _productTypeService;

        public ProductsController(IService<Product> service, IService<Category> categoryService, IService<ProductType> productTypeService)
        {
            _service = (ProductService)service;
            _categoryService = categoryService;
            _productTypeService = productTypeService;
        }

        // GET: Products
        public async Task<IActionResult> Index(string searchString, string productType, string productCategory)
        {
            IEnumerable<StockDto> items = await _service.GetItemsAsync();
            items = _service.Filter(items, searchString, productType, productCategory);
            IEnumerable<ProductType> types = await _productTypeService.GetAllAsync();
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
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

            var product = await _service.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TypeId"] = new SelectList(await _productTypeService.GetAllAsync(), "Id", "Title");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Price,TypeId,MinQuantity,IsActive")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(product);
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(await _productTypeService.GetAllAsync(), "Id", "Title", product.TypeId);
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetByIdAsync(id.Value);
            if (product == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(await _productTypeService.GetAllAsync(), "Id", "Title", product.TypeId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Price,TypeId,MinQuantity,IsActive")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(product);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await ProductExists(product.Id) == false)
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
            ViewData["TypeId"] = new SelectList(await _productTypeService.GetAllAsync(), "Id", "Title", product.TypeId);
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _service.GetByIdAsync(id.Value);
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
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductExists(int id)
        {
            return await _service.ExistsAsync(id);
        }
    }
}
