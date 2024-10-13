using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectBookShop.Models.UnitOfWork;

namespace ProjectBookShop.Areas.Admin.Pages.Publisherr
{
    public class EditModel : PageModel
    {
        private readonly IUnitOfWork _UW;

        public EditModel(IUnitOfWork uW)
        {
            _UW = uW;
        }
   
        [BindProperty]
        public ProjectBookShop.Models.Publisher publisher { get; set; }

        public async Task< IActionResult> OnGet(int? id)
        {
            if(id==null)
            {
                return NotFound();
            }
            var IsExitPublisher =await _UW.Baserepository<ProjectBookShop.Models.Publisher>().FindbyIdAsync(id);
            if (IsExitPublisher!=null)
            {
                publisher = await _UW.Baserepository<ProjectBookShop.Models.Publisher>().FindbyIdAsync(id);
                return Page();
            }else
            {
                return NotFound();
            }

         
        }
        public async Task<IActionResult> OnPost()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            _UW.Baserepository<ProjectBookShop.Models.Publisher>().Update(publisher);
            try
            {
              await  _UW.commit();
            }
            catch (DbUpdateConcurrencyException)
            {
                if(!_UW.Baserepository<ProjectBookShop.Models.Publisher>().AnyEntity(m=>m.PublisherID==publisher.PublisherID))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }

                
            }
            return RedirectToPage("./Index");

        }
    }
}
