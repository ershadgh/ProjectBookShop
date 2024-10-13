using System;
using System.Collections.Generic;
using System.Data.SqlClient;

//using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectBookShop.Models;

namespace ProjectBookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LanguagesController : Controller
    {
        private readonly BookShopContext _context;

        public LanguagesController(BookShopContext context)
        {
            _context = context;
        }

        // GET: Admin/Languages
        public async Task<IActionResult> Index()
        {
            return _context.Languages != null ?
                        View(await _context.Languages.ToListAsync()) :
                        Problem("Entity set 'BookShopContext.Languages'  is null.");
        }

        // GET: Admin/Languages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Languages == null)
            {
                return NotFound();
            }

            var language = await _context.Languages
                .FirstOrDefaultAsync(m => m.LanguageID == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // GET: Admin/Languages/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Languages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageID,LanguageName")] Language language)
        {
            if (ModelState.IsValid)
            {
                _context.Add(language);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(language);
        }

        // GET: Admin/Languages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Languages == null)
            {
                return NotFound();
            }

            var language = await _context.Languages.FindAsync(id);
            if (language == null)
            {
                return NotFound();
            }
            return View(language);
        }

        // POST: Admin/Languages/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LanguageID,LanguageName")] Language language)
        {
            if (id != language.LanguageID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(language);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LanguageExists(language.LanguageID))
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
            return View(language);
        }

        // GET: Admin/Languages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Languages == null)
            {
                return NotFound();
            }

            var language = await _context.Languages
                .FirstOrDefaultAsync(m => m.LanguageID == id);
            if (language == null)
            {
                return NotFound();
            }

            return View(language);
        }

        // POST: Admin/Languages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            if (_context.Languages == null)
            {
                return Problem("Entity set 'BookShopContext.Languages'  is null.");
            }
            //    var language = await _context.Languages.FindAsync(id);
            //    if (language != null)
            //    {
            //        _context.Languages.Remove(language);
            //    }

            //    await _context.SaveChangesAsync();
            ///اجرای دستورات اس کیو ال در .netcore
            //object[] parametr = { new SqlParameter("@id", id) };
   
         await  _context.Database.ExecuteSqlAsync($"delete from dbo.Languages where (LanguageID={id})");
         //await   _context.Database.ExecuteSqlRawAsync("delete from dbo.Languages where (LanguageID=@id)", parametr);
            return RedirectToAction(nameof(Index));
        
        }

        private bool LanguageExists(int id)
        {
            return (_context.Languages?.Any(e => e.LanguageID == id)).GetValueOrDefault();
        }
    }
}
