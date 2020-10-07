using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BSOS.Data;
using BSOS.Models;
using System.Web;
using Microsoft.AspNetCore.Session;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace BSOS.Controllers
{
    public class CustomersController : Controller
    {
        private readonly BSOSContext _context;

        public CustomersController(BSOSContext context)
        {
            _context = context;
        }

        // GET: Customers
        public async Task<IActionResult> Index()
        {
            return View(await _context.Customers.ToListAsync());
        }
        public async Task<IActionResult> Search(string firstName)
        {
            if (String.IsNullOrEmpty(firstName))
            {
                return View("Index", _context.Customers);
            }
            var result = from cust in _context.Customers where (cust.FirstName.Contains(firstName)) select cust;
            return View("Search", result);
        }

        // GET: Customers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.Include(o => o.Orders)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View(customer);
        }

        // GET: Customers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FirstName,LastName,Gender,PhoneNumber,Email,Password,Country,City,ZipCode,Address,Birthday,Orders")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                customer.Orders = new List<Order>();
                _context.Add(customer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        // GET: Customers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id);
            ViewBag.Orders = customer.Orders;
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FirstName,LastName,Gender,PhoneNumber,Email,Password,Country,City,ZipCode,Address,Birthday,Orders")] Customer customer)
        {
            if (id != customer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(customer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CustomerExists(customer.Id))
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
            return View(customer);
        }

        // GET: Customers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var customer = await _context.Customers.FindAsync(id);
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult Validate(string email, string password)
        {
            foreach (var c in _context.Customers)
                if (c.Email.Equals(email) && c.Password.Equals(password))
                    return View("Details", c);
            return View("Error");//have to create view for mistakes with the log-in
        }

        private bool CustomerExists(int id)
        {
            return _context.Customers.Any(e => e.Id == id);
        }
        public async Task<IActionResult> Filter(string Country, string City, string Gender)
        {
            var result = from c
                         in _context.Customers
                         select c;
            if (!(String.IsNullOrEmpty(Country)) && City != "city")
            {
                result = from c
                         in result
                         where (c.City.Equals(City))
                         select c;
            }
            if (!String.IsNullOrEmpty(Country) && Country != "country")
            {
                result = from c
                         in result
                         where (c.Country.Equals(Country))
                         select c;
            }
            if (!String.IsNullOrEmpty(Gender) && Gender != "gender")
            {
                result = from c
                         in result
                         where (c.Gender.Equals(Gender))
                         select c;
            }
            return View(await result.ToListAsync());
        }
        public async Task<IActionResult> CountOrders()// query that give us the customers who had at least one order and how many orders they did
        {
            var result = from c in _context.Customers.Include(o => o.Orders)
                         where c.Orders.Count > 0
                         orderby c.Orders.Count descending
                         select c;
            return View(await result.ToListAsync());
        }
        public async Task<IActionResult> MoneyThatWasPayed()
        {
            double NewTotal = 0;
            var result = (from c in _context.Customers.Include(c => c.Orders) where(1<0) select new ObjectResult()).ToList();//create empty result table
            foreach (var c in _context.Customers.Include(c=>c.Orders))
            {
                NewTotal = 0;
                if (c.Orders != null)
                {
                    foreach (var o in c.Orders)
                    {
                        NewTotal += o.TotalPrice;
                    }
                }
                result.Add(new ObjectResult() { FirstName = c.FirstName, LastName = c.LastName,Email=c.Email, Total = NewTotal });
            }
            return View(result.ToList());
        }
    }

    internal class ObjectResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double Total { get; set; }
    }
}