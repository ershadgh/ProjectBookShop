using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
//using System.Data.Entity;
using Microsoft.EntityFrameworkCore.ChangeTracking;
//using System.Data.Entity.Infrastructure;
using ProjectBookShop.Models;

namespace ProjectBookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProvincesController : Controller
    {
        private readonly BookShopContext _context;

        public ProvincesController(BookShopContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
          
          
            return _context.Provices != null ? View( _context.Provices.ToList()) :
                Problem("Entity set 'BookShopContext.Authors'  is null.");

        }

    }
}
