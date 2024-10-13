using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectBookShop.Models;

namespace ProjectBookShop.Areas.Admin.Pages.Publisher
{
    public class DetailsModel : PageModel
    {
        private readonly ProjectBookShop.Models.BookShopContext _context;

        public DetailsModel(ProjectBookShop.Models.BookShopContext context)
        {
            _context = context;
        }

        public ProjectBookShop.Models.Publisher Publisher { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Publisher = await _context.Publisher.FirstOrDefaultAsync(m => m.PublisherID == id);

            if (Publisher == null)
            {
                return NotFound();
            }
            return Page();
        }
    }
}
