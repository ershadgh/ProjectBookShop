//using Microsoft.AspNetCore.Mvc;
//using ProjectBookShop.Models;
//using System.Diagnostics;

namespace ProjectBookShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(string id)
        {
            if (id != null)
                ViewBag.ConfirmEmailAlert = "لینک فعال سازی حساب کابری به ایمیل شما ارسال شد لطفا با کلیک روی حساب خود را فعال کنید";
                    return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}