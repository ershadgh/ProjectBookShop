using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ProjectBookShop.Models;
using ProjectBookShop.Models.UnitOfWork;

namespace ProjectBookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AuthorsController : Controller
    {
        private readonly BookShopContext _context;

        private readonly IUnitOfWork _UW;

        public AuthorsController(IUnitOfWork UW, BookShopContext context)
        {
            _UW = UW;
            _context = context;
        }


        // GET: Admin/Authors
        public async Task<IActionResult> Index()
        {
            ;
            // var Authors1 = await _UW.Baserepository<Author>().FindAllAsync();
            var Authors = await _UW.Baserepository<Author>().FindByConditionAsync(a => a.FirstName.Contains("صادق"), o => o.OrderByDescending(k => k.FirstName), a => a.Author_Books);
            //var Author =await _UW.Baserepository<Author>().ToListAsync();
            ViewBag.EntitiyState = DisplyState(_context.ChangeTracker.Entries());

            //return _UW.Baserepository<Author>() != null ?
            //            View(await _UW.Baserepository<Author>().ToListAsync()) :
            //            Problem("Entity set 'BookShopContext.Authors'  is null.");


            //return Authors != null ?
            //         View(await _UW.Baserepository<Author>().FindAllAsync()) :
            //         Problem("Entity set 'BookShopContext.Authors'  is null.");

            return View(Authors);

        }

        // GET: Admin/Authors/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _UW.Baserepository<Author> == null)
            {
                return NotFound();
            }

            var author = await _UW.Baserepository<Author>()
                .FindbyIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // GET: Admin/Authors/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Authors/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AuthorID,FirstName,LastName")] Author author)
        {
            var entity = _context.Entry(author).State;
            if (ModelState.IsValid)
            {
                //_context.Attach(author).State = EntityState.Added;
                _context.Entry(author).State = EntityState.Added;
                // _context.Add(author);
                var EntitySteta = DisplyState(_context.ChangeTracker.Entries());
                await _context.SaveChangesAsync();
                ViewBag.Id = author.AuthorID;
                return RedirectToAction(nameof(Index));
            }
            return View(author);

        }
        // GET: Admin/Authors/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _UW.Baserepository<Author>() == null)
            {
                return NotFound();
            }

            var author = await _UW.Baserepository<Author>().FindbyIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        // POST: Admin/Authors/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AuthorID,FirstName,LastName")] Author author)
        {
            if (id != author.AuthorID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // _context.Attach(author).State = EntityState.Modified;
                    _context.Entry(author).State = EntityState.Modified;
                    // _context.Update(author);
                    var entitystate = DisplyState(_context.ChangeTracker.Entries());
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.AuthorID))
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
            return View(author);
        }

        // GET: Admin/Authors/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _UW.Baserepository<Author>() == null)
            {
                return NotFound();
            }

            var author = await _UW.Baserepository<Author>().FindbyIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return View(author);
        }

        // POST: Admin/Authors/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_UW.Baserepository<Author>() == null)
            {
                return Problem("Entity set 'BookShopContext.Authors'  is null.");
            }
            var author = await _UW.Baserepository<Author>().FindbyIdAsync(id);
            if (author != null)
            {
                _UW.Baserepository<Author>().Deleted(author);
                var entitystate = DisplyState(_context.ChangeTracker.Entries());
                await _UW.commit();
            }


            return RedirectToAction(nameof(Index));
        }

        private bool AuthorExists(int id)
        {
            //return (_UW.Baserepository<Author>()?.Any(e => e.AuthorID == id)).GetValueOrDefault();
            return (_UW.Baserepository<Author>()?.AnyEntity(a => a.AuthorID == id)).GetValueOrDefault();
        }
        public List<EntityStates> DisplyState(IEnumerable<EntityEntry> entities)
        {
            List<EntityStates> entityStates = new List<Models.EntityStates>();
            foreach (var entry in entities)
            {
                EntityStates states = new EntityStates()
                {
                    EntityName = entry.Entity.GetType().Name,
                    EntitySate = entry.State.ToString()
                };
                entityStates.Add(states);
            }
            return entityStates;
        }
        public async Task<IActionResult> AuthorBooks(int id)
        {

            //   var author = await _UW.Baserepository<Author>().Where(a => a.AuthorID == id).FirstOrDefaultAsync();
            var author = await _UW.Baserepository<Author>().FindbyIdAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            else
            {
                return View(author);
            }


        }
    }
}
