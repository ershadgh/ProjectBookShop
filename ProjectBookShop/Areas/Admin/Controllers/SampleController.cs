using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging.Signing;

namespace ProjectBookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SampleController : Controller
    {
        private readonly BookShopContext _context;

        public SampleController(BookShopContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var quuery = EF.CompileQuery ((BookShopContext context, int id) => context.Books.SingleOrDefault(b=>b.BookID==id));

            Stopwatch sw = new Stopwatch();
            sw.Start();
            for (int i = 1; i < 1000; i++)
            {
                var book = quuery(_context, i);
               // var book = _context.Books.SingleOrDefault(b => b.BookID == i);

            }
            sw.Stop();
            return View(sw.ElapsedMilliseconds);
        }
    }
}
