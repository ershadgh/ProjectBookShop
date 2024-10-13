using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectBookShop.Models.UnitOfWork;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjectBookShop.Areas.Admin.Pages.Publisherr
{
    public class CreateModel : PageModel
    {
        private readonly IUnitOfWork _UW;

        public CreateModel(IUnitOfWork uW)
        {
            _UW = uW;
        }

        [DisplayName("ناشر")]
        [Required(ErrorMessage = "لطفا نام {0} را وارد کنید.")]
        [BindProperty]
        public ProjectBookShop.Models.Publisher Publisher { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if(!ModelState.IsValid)
            {
                return Page();
            }
            await _UW.Baserepository<ProjectBookShop.Models.Publisher>().Create(Publisher);
            await _UW.commit();
            return RedirectToPage("./Index");
        }
    }
}
