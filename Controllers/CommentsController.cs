using System;
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
    public class CommentsController : Controller
    {
        private readonly BSOSContext _context;

        public CommentsController(BSOSContext context)
        {
            _context = context;
        }

        // GET: Comments
        public async Task<IActionResult> Index()
        {
            return View(await _context.Comment.ToListAsync());
        }
        public async Task<IActionResult> Search(string sentBy)
        {
            if (String.IsNullOrEmpty(sentBy))
            {
                return View("Index", _context.Comment);
            }
            var result = from com in _context.Comment where (com.SentBy.Contains(sentBy)) select com;
            return View("Search", result);
        }

        // GET: Comments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // GET: Comments/Create
        public IActionResult Create(int? productID)
        {
            ViewData["productID"] = productID;
            ViewBag.Date = DateTime.Now;
            ViewBag.IP = HttpContext.Connection.RemoteIpAddress;
            ViewBag.Products = new SelectList(_context.Products.ToList(), "ProductId", "ProductName");
            return View();
        }

        // POST: Comments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,SentBy,Posted,IP")] Comment comment, int ProductId)
        {
            if (comment.Title!=null&&comment.SentBy!=null&&comment.Body!=null)
            {
                comment.Posted = DateTime.Now;
                comment.IP = HttpContext.Connection.RemoteIpAddress.ToString();
                comment.Product = _context.Products.First(p => p.ProductId == ProductId);
                _context.Add(comment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(comment);
        }

        // GET: Comments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment.FindAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return View(comment);
        }

        // POST: Comments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,SentBy,Posted,IP,ProductId")] Comment comment,int ProductId)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (comment.Title != null && comment.Body != null)//ModelState.IsValid
            {
                try
                {
                    comment.Posted = DateTime.Now;
                    comment.Product = _context.Products.Find(comment.ProductId);
                    _context.Update(comment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CommentExists(comment.Id))
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
            return View(comment);
        }

        // GET: Comments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = await _context.Comment
                .FirstOrDefaultAsync(m => m.Id == id);
            if (comment == null)
            {
                return NotFound();
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var comment = await _context.Comment.FindAsync(id);
            _context.Comment.Remove(comment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CommentExists(int id)
        {
            return _context.Comment.Any(e => e.Id == id);
        }
    }
}
