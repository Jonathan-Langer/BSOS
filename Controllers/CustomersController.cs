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
using System.Net;
using Microsoft.AspNetCore.Session;
using Microsoft.AspNetCore.Authorization;
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

        public async Task<IActionResult> MyAccount()
        {
            if (Customer.CustomersId.Count==0)
            {
                return NotFound();
            }
            int id = Customer.CustomersId.Peek();
            var customer = await _context.Customers.Include(o => o.Orders)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (customer == null)
            {
                return NotFound();
            }

            return View("Details",customer);
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
                customer.Orders.Add(new Order() { IsShoppingCart = true });
                _context.Add(customer);
                await _context.SaveChangesAsync();
                if (Customer.CustomersId.Count == 0)
                    Customer.CustomersId = new Stack<int>();
                Customer.CustomersId.Push(customer.Id);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }
        public IActionResult AddToCart(int ProductId, int Amount)
        {
               if (Customer.CustomersId == null)
               {
                   return View("~/Views/Home/Login.cshtml");
               }
               int CustomerId = Customer.CustomersId.Peek();
               var customer = _context.Customers.Include(o => o.Orders).ThenInclude(po=>po.ProductOrders).Where(c => c.Id == CustomerId).FirstOrDefault();
               var Product = _context.Products.Find(ProductId);
               if(customer!=null&&Product!=null)
                {
                if (customer.Orders.Count == 0 || customer.Orders.Where(o => o.IsShoppingCart).FirstOrDefault() == null)
                    customer.Orders.Add(new Order() { IsShoppingCart = true, TotalPrice = 0 }) ;
                    var ShoppingCart = customer.Orders.Where(o => o.IsShoppingCart).FirstOrDefault();
                    if (ShoppingCart.ProductOrders == null)
                        ShoppingCart.ProductOrders = new List<ProductOrder>();
                    if(ShoppingCart.ProductOrders.Where(p => p.ProductId == ProductId).FirstOrDefault()!=null)
                    {
                        
                        if (ShoppingCart.ProductOrders.Where(p => p.ProductId == ProductId).FirstOrDefault().Amount >= 1)
                        {

                            ShoppingCart.ProductOrders.Where(p => p.ProductId == ProductId).FirstOrDefault().Amount += Amount;
                            _context.ProductOrder.Update(ShoppingCart.ProductOrders.Where(p => p.ProductId == ProductId).FirstOrDefault());
                            ShoppingCart.TotalPrice += Product.Price * Amount;
                            _context.SaveChanges();
                             _context.Orders.Update(ShoppingCart);
                            _context.SaveChanges();
                        }
                    }
                    else
                    {
                    ShoppingCart.ProductOrders.Add(new ProductOrder() { ProductId = ProductId, Product = _context.Products.Find(ProductId), Order = null, Amount = 1 });

                        ShoppingCart.TotalPrice += Product.Price * Amount;
                        _context.Orders.Update(ShoppingCart);
                         _context.SaveChanges();
                    }
                       
                    _context.Customers.Update(customer);
                    _context.SaveChanges();
                }
            return View("~/Views/Products/Index.cshtml", _context.Products.ToList());
        }
        public void DeleteFromCart(int ProductId)
        {
            int CustomerId = Customer.CustomersId.Peek();
            var customer = _context.Customers.Include(o => o.Orders)
                .ThenInclude(po => po.ProductOrders).Where(c => c.Id == CustomerId).FirstOrDefault();
            var Product = _context.Products.Find(ProductId);
            var AllProductsInShoppingCart = _context.ProductOrder.Include(o => o.Order)
                .Where(po => (po.Order.CustomerId == CustomerId && po.Order.IsShoppingCart));
            var ProductOrder = AllProductsInShoppingCart.Where(po => po.ProductId == ProductId).FirstOrDefault();
            if (customer != null && Product != null&& ProductOrder!=null)
            {
                var ShoppingCart = customer.Orders.Where(o => o.IsShoppingCart).FirstOrDefault();
                if (ProductOrder.Amount>1)
                {
                    ShoppingCart.ProductOrders.Where(p => p.ProductId == ProductId).FirstOrDefault().Amount -= 1;
                    ProductOrder.Amount -=1;
                    _context.ProductOrder.Update(ProductOrder);
                    _context.SaveChanges();
                    _context.Customers.Update(customer);
                    _context.SaveChanges();
                    _context.Orders.Update(ShoppingCart);
                    _context.SaveChanges();
                }
                else
                {
                    ShoppingCart.ProductOrders.Remove(ProductOrder);
                    _context.ProductOrder.Remove(ProductOrder);
                    _context.SaveChanges();
                    _context.Customers.Update(customer);
                    _context.SaveChanges();
                    _context.Orders.Update(ShoppingCart);
                    _context.SaveChanges();
                    if (_context.ProductOrder.Include(o => o.Order)
                    .Where(po => (po.Order.CustomerId == CustomerId && po.Order.IsShoppingCart)).FirstOrDefault() != null)
                    {
                        if(_context.ProductOrder.Find(ProductOrder.ProductId,ProductOrder.OrderId)!=null)
                        {
                            _context.ProductOrder.Remove(ProductOrder);
                            _context.SaveChanges();
                        }
                    }
                }
            }
        }

        public IActionResult GetShoppingCart()
        {
            if(Customer.CustomersId==null)
            {
                return View("~/Views/Home/Login.cshtml");
            }
            int CustomerId = Customer.CustomersId.Peek();
            var customer = _context.Customers.Include(o => o.Orders).ThenInclude(po => po.ProductOrders).Where(c => c.Id == CustomerId).FirstOrDefault();
            var ListOfOrders = _context.Customers.Include(o => o.Orders).ThenInclude(po => po.ProductOrders).ThenInclude(p => p.Product).Where(c => c.Id == CustomerId).Select(c => c.Orders).FirstOrDefault();
            if(ListOfOrders!=null)
            {
                foreach (Order o in ListOfOrders)
                {
                    if (o.IsShoppingCart == true)
                    {
                        var result = o;
                        return View("ShoppingCart", result);
                    }
                }
            }
            return View("EmptyShopCart");
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
                {
                    if (Customer.CustomersId == null)
                        Customer.CustomersId = new Stack<int>();
                    Customer.CustomersId.Push(c.Id);
                    if (c.Roles == 0)
                        ViewBag.IsAdmin = "Admin";
                    //return View("~/Views/Home/Shop.cshtml",c);
                    return View("Details", c);
                }
            ViewBag.IsAdmin = "Customer";
            ViewBag.Message = "wrong details";
            return View("~/Views/Home/Login.cshtml");//have to create view for mistakes with the log-in
        }
        public string GetFirstName(int id)
        {
            var Customer = _context.Customers.Where(c => c.Id == id).FirstOrDefault();
            return Customer.FirstName;
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

        public IActionResult MoneyThatWasPayed()
        {
            double NewTotal = 0;
            var result = (from c in _context.Customers.Include(c => c.Orders) where(1<0) select new ObjectsResult()).ToList();//create empty result table
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
                result.Add(new ObjectsResult() { FirstName = c.FirstName, LastName = c.LastName,Email=c.Email, Total = NewTotal });
            }
            ViewBag.Data = result;
            return View(result.ToList());
        }

        public string GetEmail(int CustomerId)
        {
            var Customer = _context.Customers.Where(c => c.Id == CustomerId).FirstOrDefault();
            return Customer.Email;
        }

        public IActionResult ShopCart()
        {
            return View("ShoppingCart");
        }
    }

    public class ObjectsResult
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public double Total { get; set; }
    }
}