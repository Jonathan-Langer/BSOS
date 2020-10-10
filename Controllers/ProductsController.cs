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
using System.Collections.ObjectModel;
using System.Threading;

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
            //if (category.Equals("Men"))
            //{
            //    result = from pro
            //             in result
            //             where (!pro.Category.Contains("Women"))
            //             select pro;
            //}
            return View("Index", result);
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
            return View("Index", await result.ToListAsync());
        }
        internal class ObjectResult
        {
            public string Brand { get; set; }
            public int count { get; set; }
        }
        public int AmountOfComments(Product pro)
        {
            return pro.Comments.Count();
        }
        public async Task<IActionResult> SortByCategory(string Category)
        {
            var result = from p
                     in _context.Products
                         select p;
            if (!String.IsNullOrEmpty(Category) && Category != "category")
            {
                result = from pro
                         in result
                         where (pro.Category.Contains(Category))
                         select pro;
            }
            if(Category.Equals("Men"))
            {
                result = from pro
                         in result
                         where (!pro.Category.Contains("Women"))
                         select pro;
            }
            return View("Index",await result.ToListAsync());
        }

        public async Task<IActionResult> ShowAllProducts()
        {
            var result = from p
                   in _context.Products
                         select p;
            return View("Index", await result.ToListAsync());
        }
        [HttpGet]
        public ActionResult Statistics()
        {
            //statistic 1- how many orders every customer had made, there is only one shopping cart
            ICollection<Stat> statistic1 = new Collection<Stat>();
            var result1 = from c in _context.Customers.Include(o => o.Orders)
                         where (c.Orders.Count-1) > 0
                         orderby (c.Orders.Count) descending
                         select c;
            foreach (var v in result1)
            {
                statistic1.Add(new Stat(v.FirstName, v.Orders.Count()));
            }

            ViewBag.data = statistic1;
            //finish first statistic
            //statistic 2- which brand the customers prefer to order
            ICollection<Stat> statistic2 = new Collection<Stat>();

            int Count;
            var result2 = (from p in _context.Products where (1 < 0) select new ObjectResult()).ToList();//create empty result table
            foreach (var pro in _context.Products.Include(po => po.ProductOrders).ThenInclude(o => o.Order))
            {
                Count = 0;
                if (pro == null)
                    continue;
                foreach (var po in pro.ProductOrders)
                {
                    if (po == null)
                        continue;
                    if (po.Product.Brand == pro.Brand)
                        ++Count;
                }
                result2.Add(new ObjectResult() { Brand = pro.Brand, count = Count });
            }
            foreach (var v in result2)
            {
                if(v.count>0)
                    statistic2.Add(new Stat(v.Brand, v.count));
            }

            ViewBag.data2 = statistic2;

            return View();
        }
    }
    public class Stat
    {
        public string Key;
        public int Values;
        public Stat(string key, int values)
        {
            Key = key;
            Values = values;
        }
    }
}
