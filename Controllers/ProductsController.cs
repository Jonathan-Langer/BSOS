using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BSOS.Data;
using BSOS.Models;
using Microsoft.AspNetCore.Routing.Template;
using Microsoft.EntityFrameworkCore.Storage;
using SQLitePCL;

namespace BSOS.Controllers
{
    public class ProductsController : Controller
    {
        private readonly BSOSContext _context;

        public ProductsController(BSOSContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await _context.Products.ToListAsync());
        }
        public async Task<IActionResult> Search(string name)
        {
            if (String.IsNullOrEmpty(name))
            {
                return View("Index", _context.Products);
            }
            var result = from pro in _context.Products where (pro.ProductName.Contains(name)) select pro;
            return View("Search", result);
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.Include(p => p.Comments)
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Price,Size,Brand,Color,Gender")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }
        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Price,Size,Brand,Color,Gender")] Product product)
        {
            if (id != product.ProductId)
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
                    if (!ProductExists(product.ProductId))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
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
            var product = await _context.Products.FindAsync(id);
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
            return _context.Products.Any(e => e.ProductId == id);
        }
        public async Task<IActionResult> FilterByBrand(string value)
        {
            var result = from pro in _context.Products where (pro.Brand.Contains(value)) select pro;
            return View(await result.ToListAsync());
        }
        private async Task<IActionResult> FilterByColor(string value)
        { 
            var result = from pro in _context.Products where (pro.Color.Contains(value)) select pro;
            return View(await result.ToListAsync());
        }
        private async Task<IActionResult> FilterBySize(string value)
        {
            var result = from pro in _context.Products where (pro.Size.Contains(value)) select pro;
            return View(await result.ToListAsync());
        }
        public async Task<IActionResult> Filter(string RequierdAttribute,string AttributeValue)
        {
            //var result = from p in _context.Products select p;
            BSOSContext db = new BSOSContext();
            switch(RequierdAttribute)
            {
                case "Brand":
                    return await FilterByBrand(AttributeValue);
                    break;
                case "Color":
                    FilterByColor(db, AttributeValue);
                    break;
                case "Size":
                    
                    break;
                default:
                    break;
                    
            }
            return View("Error");
        }
    }
}
