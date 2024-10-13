using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ProjectBookShop.Models;

namespace TestApi.Controllers
{
    public class ApiTestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Callweb()
        {
            List<Language> languages = new List<Language>();
            using(var httpclient=new HttpClient())
            {
                using(var response =await httpclient.GetAsync("http://localhost:5024/api/BookApi/GetAllBook"))
                {
                    string Apilanguage = await response.Content.ReadAsStringAsync();
                     languages = JsonConvert.DeserializeObject<List<Language>>(Apilanguage);
                }
            }
            return View(languages);
        }
    }
}
