﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BSOS.Data;
using BSOS.Models;

namespace BSOS.Controllers
{
    public class OrdersController : Controller
    {
        private readonly BSOSContext _context;
        public OrdersController(BSOSContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index()
        {
            return View(await _context.Orders.ToListAsync());
        }
        public async Task<IActionResult> Search(string orderId)
        {
            if (String.IsNullOrEmpty(orderId))
            {
                return View("Index", _context.Orders);
            }
            var result = from o in _context.Orders where (o.OrderID.ToString().Contains(orderId)) select o;
            return View("Search", result);
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(c => c.Customer)
                .Include(po => po.ProductOrders).ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create(int? CustomerId)
        {
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            ViewData["IdCustomer"] = CustomerId; 
            ViewBag.OrderDate = DateTime.Now;
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderID,OrderDate,CustomerId")] Order order, int CustomerId, int[] ProductId)
        {
            if (ModelState.IsValid)
            {
                order.ProductOrders = new List<ProductOrder>();
                foreach (var id in ProductId)//insert products into details
                {
                    order.ProductOrders.Add(new ProductOrder() { ProductId = id, OrderId = order.OrderID, Product = _context.Products.Find(id),Order=order });
                }
                await _context.SaveChangesAsync();
                order.Customer = _context.Customers.First(c => c.Id == CustomerId);
                order.TotalPrice = 0;
                order.OrderDate = DateTime.Now;
                foreach (var pro in order.ProductOrders)
                {
                    order.TotalPrice += pro.Product.Price;
                }
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                order.ProductOrders = new List<ProductOrder>();
                foreach (var id in ProductId)
                {
                    order.ProductOrders.Add(new ProductOrder() { ProductId = id, OrderId = order.OrderID, Product = _context.Products.Find(id), Order = order });
                    //_context.ProductOrder.Add(new ProductOrder() { ProductId = id, OrderId = order.OrderID, Product = _context.Products.Find(id), Order = order });
                }
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                order.Customer.Orders.Add(order);
                _context.Orders.Update(order);
                await _context.SaveChangesAsync();
                _context.Customers.Update(order.Customer);
                await _context.SaveChangesAsync();
                foreach (var id in ProductId)
                    _context.Products.Find(id).ProductOrders.Add(_context.ProductOrder.Find(id, order.OrderID));
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            ViewData["ProductId"] = new SelectList(_context.Products, "ProductId", "ProductName");
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("OrderID,OrderDate,TotalPrice,CustomerId")] Order order, int[] ProductId)
        {
            if (id != order.OrderID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    order.ProductOrders = new List<ProductOrder>();
                    foreach (var idPro in ProductId)
                    {
                        order.ProductOrders.Add(new ProductOrder() { ProductId = idPro, OrderId = order.OrderID, Product = _context.Products.Find(idPro), Order = order });
                    }
                    foreach (var po in order.ProductOrders)
                    {
                        _context.ProductOrder.AddRange(po);
                    }
                    order.OrderDate = DateTime.Now;
                    foreach (var pro in order.ProductOrders)
                    {
                        order.TotalPrice += pro.Product.Price;
                    }
                    _context.Update(order);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.OrderID))
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
            //ViewData["CustomerId"] = new SelectList(_context.Customers, "Id", "Address", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.Include(c => c.Customer)
                .Include(po => po.ProductOrders).ThenInclude(p => p.Product)
                .FirstOrDefaultAsync(m => m.OrderID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            order.Customer.Orders.Remove(order);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.OrderID == id);
        }
    }
}
