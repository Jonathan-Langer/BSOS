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
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
        public async Task<IActionResult> Create([Bind("ProductId,ProductName,Price,Size,Brand,Color,Category")] Product product)
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
        public async Task<IActionResult> Edit(int id, [Bind("ProductId,ProductName,Price,Size,Brand,Color,Category")] Product product)
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

        public async Task<IActionResult> Filter(string Brand, string Size, string Color)
        {
            var result = from p
                         in _context.Products 
                         select p;
            if (!(String.IsNullOrEmpty(Brand))&&Brand!="brand")
            {
                result = from pro
                         in result
                         where (pro.Brand.Equals(Brand)) 
                         select pro;
            }
            if (!String.IsNullOrEmpty(Size)&&Size!="size")
            {
                result = from pro
                         in result
                         where (pro.Size.Equals(Size))
                         select pro;
            }
            if (!String.IsNullOrEmpty(Color)&&Color!="color")
            {
                result = from pro 
                         in result
                         where (pro.Color.Equals(Color))
                         select pro;
            }
            return View(await result.ToListAsync());
        }
        public async Task<IActionResult> AmountOfOrdersPerProduct()
        {
            int Count;
            var result = await (from p in _context.Products where (1 < 0) select new ObjectResult()).ToListAsync();//create empty result table
            foreach (var p in _context.Products.Include(po => po.ProductOrders).ThenInclude(o => o.Order))
            {
                Count = 0;
                foreach (var c in _context.Customers.Include(c => c.Orders).ThenInclude(po => po.ProductOrders))
                {
                    if (c.Orders == null)
                        continue;
                    foreach (var o in c.Orders)
                    {
                        if (o == null)
                            continue;
                        foreach (var po in o.ProductOrders)
                            if (po.ProductId == p.ProductId)
                                ++Count;
                    }
                }
                result.Add(new ObjectResult() { Brand = p.Brand, ProductName = p.ProductName, price = p.Price, count = Count });
            }
            return View(result.ToList());
        }
        internal class ObjectResult
        {
            public string Brand { get; set; }
            public string ProductName { get; set; }
            public double price { get; set; }
            public int count { get; set; }
        }
    }
}
