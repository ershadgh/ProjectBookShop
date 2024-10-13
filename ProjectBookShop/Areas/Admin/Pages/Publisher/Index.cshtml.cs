using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ProjectBookShop.Models;
using ProjectBookShop.Models.UnitOfWork;

namespace ProjectBookShop.Areas.Admin.Pages.Publisher
{
    public class IndexModel : PageModel
    {
        private readonly ProjectBookShop.Models.BookShopContext _context;

        public IndexModel(ProjectBookShop.Models.BookShopContext context)
        {
            _context = context;
        }


        public IList<ProjectBookShop.Models.Publisher> Publisher { get;set; }

        public async Task OnGetAsync()
        {
            Publisher = await _context.Publisher.ToListAsync();
          
        }
    }
}
