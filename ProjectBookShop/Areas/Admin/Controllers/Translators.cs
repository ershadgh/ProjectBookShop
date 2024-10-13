using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBookShop.Models;
using ProjectBookShop.Models.ViewModels;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace ProjectBookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class Translators : Controller
    {
        private readonly BookShopContext _context;
        public Translators(BookShopContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return _context.Translator != null ?
                View(await _context.Translator.ToListAsync()) :
                Problem("Entity set 'BookShopContext.Translator'  is null.");



        }
        [HttpGet]

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TranslatorCreateViewModel ViewModel)
        {
            

            if (ModelState.IsValid)
            {
                Translator translator = new Translator()
                {
                   Name=ViewModel.Name,
                   Family=ViewModel.Family,

                };
                _context.Add(translator);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");

            }
            return View(ViewModel);
        }
        [HttpGet]
        public async Task<ActionResult> Edit(int? id)
        {
          
            TranslatorCreateViewModel viewmodel1 = new TranslatorCreateViewModel();
            if (id == null || _context.Translator==null)
            {
                return NotFound();
            }

            var Translator = await _context.Translator.FindAsync(id);
            
            if (Translator != null)
            {
                TranslatorCreateViewModel viewmodel = new TranslatorCreateViewModel();


                viewmodel.TranslatorID = Translator.TranslatorID;
                viewmodel.Name = Translator.Name;
                viewmodel.Family = Translator.Family;
                viewmodel1 = viewmodel;

            }
           
            
            return View(viewmodel1);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public  async Task<ActionResult> Edit(int id,TranslatorCreateViewModel translatorviewmodel)
        {
            if (id!=translatorviewmodel.TranslatorID)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    Translator translator = new Translator()
                    {
                        TranslatorID=translatorviewmodel.TranslatorID,
                        Name=translatorviewmodel.Name,
                        Family=translatorviewmodel.Family,
                    };
                    _context.Update(translator);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                   if(!TranslatorExists(id))
                    {
                        return NotFound();
                    }else
                    {
                        throw;
                    }

                }
              
            }
            return RedirectToAction(nameof(Index));


        }
        [HttpGet]
        public async Task<ActionResult> Delete(int? id)
        {
           
            if (id == null || _context.Translator == null)
                return NotFound();

          var transtor=await  _context.Translator.FirstOrDefaultAsync(t=>t.TranslatorID==id);
           if (transtor == null)
            {
                return NotFound();
            }
            return View(transtor);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int TranslatorID)
        {
            if (_context.Translator == null)
                return Problem("Entity set 'BookShopContext.Translator'  is null.");

            var translator = await _context.Translator.FindAsync(TranslatorID);

            if (translator != null)
            {
                _context.Remove(translator);

               
            }
            _context.SaveChanges();
            return RedirectToAction(nameof(Index));
            



        }
        public bool TranslatorExists(int id)
        {
            return (_context.Translator?.Any(t=>t.TranslatorID==id)).GetValueOrDefault();
        }

    }
}
