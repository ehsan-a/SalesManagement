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
using System.Net.Sockets;
using System.Threading.Tasks;

namespace SalesManagement.Controllers
{
    public class ProductTypesController : Controller
    {
        private readonly IProductTypeService _service;
        private readonly IService<Category> _categoryService;

        public ProductTypesController(IProductTypeService service, IService<Category> categoryService)
        {
            _service = service;
            _categoryService = categoryService;
        }

        // GET: ProductTypes
        public async Task<IActionResult> Index(string searchString, string productTypeCategory)
        {
            IEnumerable<ProductType> items = await _service.GetAllAsync();
            items = _service.Filter(items, searchString, productTypeCategory);
            IEnumerable<Category> categories = await _categoryService.GetAllAsync();
            var vm = new ProductTypeIndexViewModel
            {
                SelectListCategory = new SelectList(categories, "Id", "Title", productTypeCategory),
                Items = items.ToList(),
                SearchString = searchString,
            };
            return View(vm);
        }

        // GET: ProductTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _service.GetByIdAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // GET: ProductTypes/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Title");
            return View();
        }

        // POST: ProductTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,CategoryId")] ProductType productType)
        {
            if (ModelState.IsValid)
            {
                await _service.CreateAsync(productType);
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Title", productType.CategoryId);
            return View(productType);
        }

        // GET: ProductTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _service.GetByIdAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Title", productType.CategoryId);
            return View(productType);
        }

        // POST: ProductTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,CategoryId")] ProductType productType)
        {
            if (id != productType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _service.UpdateAsync(productType);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await ProductTypeExists(productType.Id) == false)
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
            ViewData["CategoryId"] = new SelectList(await _categoryService.GetAllAsync(), "Id", "Id", productType.CategoryId);
            return View(productType);
        }

        // GET: ProductTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var productType = await _service.GetByIdAsync(id.Value);
            if (productType == null)
            {
                return NotFound();
            }

            return View(productType);
        }

        // POST: ProductTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> ProductTypeExists(int id)
        {
            return await _service.ExistsAsync(id);
        }
    }
}
