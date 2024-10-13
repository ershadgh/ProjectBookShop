using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProjectBookShop.Models.UnitOfWork;

namespace ProjectBookShop.Areas.Admin.Pages.Publisherr
{
    public class IndexModel : PageModel
    {
        private readonly IUnitOfWork _WU;

        public IndexModel(IUnitOfWork wU)
        {
            _WU = wU;
        }
        public IEnumerable<ProjectBookShop.Models.Publisher> Publishers { get; set; }
        [BindProperty(SupportsGet =true)]
        public int CurruntPage { get; set; }
        public int Count { get; set; }
        public int PageSize { get; set; } = 3;
        public int TotalPages =>(int)Math.Ceiling(decimal.Divide(Count, PageSize));

        public bool ShowPrevious => CurruntPage > 1;
        public bool ShowNext => CurruntPage < TotalPages;
        public async Task<IActionResult> OnGet()
        {
            Count = _WU.Baserepository<ProjectBookShop.Models.Publisher>().GetCount();
            // Publishers = await _WU.Baserepository<ProjectBookShop.Models.Publisher>().FindAllAsync();
            Publishers = await _WU.Baserepository<ProjectBookShop.Models.Publisher>().GetPaginteResualtAsync(CurruntPage, PageSize);
            return Page();
        }
        public async Task<IActionResult> OnPostDeletedAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            else
            {
                var PublisherD = await _WU.Baserepository<ProjectBookShop.Models.Publisher>().FindbyIdAsync(id);
                if (PublisherD == null)
                {
                    return NotFound();

                }
                else
                {
                    _WU.Baserepository<ProjectBookShop.Models.Publisher>().Deleted(PublisherD);
                    await _WU.commit();
                    return RedirectToPage("./Index");
                }
            }



        }
    }
}
