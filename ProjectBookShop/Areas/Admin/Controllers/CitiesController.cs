using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjectBookShop.Models;
using ProjectBookShop.Models.UnitOfWork;
using System.Security.Authentication;

namespace ProjectBookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CitiesController : Controller
    {

        private readonly IUnitOfWork _UW;

        public CitiesController(IUnitOfWork uW)
        {
            _UW = uW;
        }

        public async Task<IActionResult> Index(int id)
        {

            if (id == 0)
            {
                return NotFound();
            }
            else
            {

                //explicit loding with query
                var Provice = await _UW.Baserepository<Provice>().FindbyIdAsync(id);
                ///  _context.Entry( Provice).Collection(c => c.Cities).Query().Where(c => c.CityName.Contains("جوانرود")).Load();
                _UW._context.Entry(Provice).Collection(c => c.Cities).Load();

                return View(Provice);
            }

            // var cityy = _context.Cities.Where(a => a.ProviceID == id).ToList();
            // var city = (from c in _context.Cities where (c.ProviceID == id) select new City { CityName = c.CityName }).ToList();

        }
        [HttpGet]
        public async Task<IActionResult> Craete(int id)
        {
            City city = new City() { ProviceID = id };
            return View(city);
        }
        [HttpPost]
        public async Task<IActionResult> Craete([Bind("CityID,CityName,ProviceID")] City city)
        {
            if (ModelState.IsValid)
            {
                Random random = new Random();

                int RandomID = random.Next(331, 1000);
                var ExitID = await _UW.Baserepository<City>().FindbyIdAsync(RandomID);
                while (ExitID != null)
                {
                    RandomID = random.Next(331, 1000);
                    ExitID = await _UW.Baserepository<City>().FindbyIdAsync(RandomID);
                }
                City cityy = new City()
                {
                    CityID = RandomID,
                    CityName = city.CityName,
                    ProviceID = city.ProviceID
                };
                _UW.Baserepository<City>().Create(cityy);
                _UW.commit();
                return RedirectToAction(nameof(Index), new { id = city.ProviceID });
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var City = await _UW.Baserepository<City>().FindbyIdAsync(id);
            if (City == null)
            {
                return NotFound();
            }
            else
            {
                return View(City);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("CityID,CityName,ProviceID")] City city)
        {
            if (id != city.CityID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _UW.Baserepository<City>().Update(city);
                _UW.commit();
                return RedirectToAction("Index", new { id = city.ProviceID });
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if (id == null || _UW.Baserepository<City>() == null)
            {
                return NotFound();
            }
            var city = await _UW.Baserepository<City>().FindbyIdAsync(id);
            if(city!=null)
            {
                return View(city);
            }
            return View();
        }
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if(_UW.Baserepository<City>()==null)
            {
                return Problem("Entity set 'BookShopContext.City'  is null.");
            }
            var city =await _UW.Baserepository<City>().FindbyIdAsync(id);
            if(city!=null)
            {
                _UW.Baserepository<City>().Deleted(city);
                _UW.commit();
                return RedirectToAction(nameof(Index), new { id = city.ProviceID });
            }
            return View();

        }
        public async Task<IActionResult> Detail(int id)
        {

            var city =await _UW.Baserepository<City>().FindbyIdAsync(id);
            return View(city);
        }
    }
}
