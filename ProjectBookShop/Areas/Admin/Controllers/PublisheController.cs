using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using ProjectBookShop.Models;
using ProjectBookShop.Models.UnitOfWork;
using ProjectBookShop.Models.ViewModels;
using System.Collections.Immutable;
using System.Security.Cryptography.Pkcs;

namespace ProjectBookShop.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PublisheController : Controller
    {
        private readonly IUnitOfWork _UW;
        private readonly BookShopContext _context;
        public PublisheController(IUnitOfWork uW)
        {
            _UW = uW;
        }
        public async Task<IActionResult> Index()
        {

            List<Publisherviewmodel> Publisherviewmodels = new List<Publisherviewmodel>();
            var publisher = await _UW.Baserepository<ProjectBookShop.Models.Publisher>().FindAllAsync();
            var publisherT = publisher.Select(a=> new Publisherviewmodel { PublisherID = a.PublisherID, PublisherName = a.PublisherName ,chartDatas1=a.ChartDatas } ).Where(a=>a.PublisherID==3 || a.PublisherID==1002).ToList()  ;


            var ChartData = _UW._context.ChartDatas.Include(b => b.Publisher).Select(a => new ChartDataVM { SentCount = a.SentCount, DateTime = a.DateTime,UserID=a.UserID }).ToList();
            var ChartGroup = ChartData.GroupBy(a => a.PublisherID).Select(g => new Publisherviewmodel { UserId = g.Key, chartDatas = g.ToList() }).ToList();
            //ViewBag.DataChart = ChartData.GroupBy(a => a.PublisherID).Select(g => new Publisherviewmodel { UserId = g.Key, chartDatas = g.ToList() }).ToList();

          var data = publisherT.SelectMany(x =>x.chartDatas1).GroupBy(x=> x.PublisherID).ToList();
            
            TempData["www"] = data;



            //foreach (var item in collection)
            //{

            //}
            //  ViewBag.datachart = JsonConvert.SerializeObject(ChartGroup);

            //foreach (var item in publisherT)
            //{
            //    Publisherviewmodel Publisherviewmodel1 = new Publisherviewmodel()
            //    { datachart = new int[6],PublisherID=item.PublisherID, PublisherName = item.PublisherName };

            //    Publisherviewmodel1.datachart[0] = 2;
            //    Publisherviewmodel1.datachart[1] = 19;
            //    Publisherviewmodel1.datachart[2] = 3;
            //    Publisherviewmodel1.datachart[3] = 5;
            //    Publisherviewmodel1.datachart[4] = 2;
            //    Publisherviewmodel1.datachart[5] = 3;
            //    TempData["data"] = JsonConvert.SerializeObject(Publisherviewmodel1);
            //    Publisherviewmodels.Add(Publisherviewmodel1);
            // }
        foreach (var item in publisherT)
            {
                Publisherviewmodel Publisherviewmodel1 = new Publisherviewmodel()
                { datachart = new int[6], PublisherID = item.PublisherID, PublisherName = item.PublisherName };

        Publisherviewmodel1.datachart[0] = 2;
                Publisherviewmodel1.datachart[1] = 19;
                Publisherviewmodel1.datachart[2] = 3;
                Publisherviewmodel1.datachart[3] = 5;
                Publisherviewmodel1.datachart[4] = 2;
                Publisherviewmodel1.datachart[5] = 3;
                TempData["data"] = JsonConvert.SerializeObject(Publisherviewmodel1);
                Publisherviewmodels.Add(Publisherviewmodel1);
            }
        



            return View(publisherT);
        }
        [HttpGet]
        public async Task<JsonResult> GetChartData1()
        {

            List<Publisherviewmodel> Publisherviewmodels = new List<Publisherviewmodel>();
            var publisher = await _UW.Baserepository<ProjectBookShop.Models.Publisher>().FindAllAsync();

            //foreach (var item in publisher)
            //{
            //    Publisherviewmodel Publisherviewmodel1 = new Publisherviewmodel()
            //    { datachart = new int[] {2,19,3,5,2,3}, PublisherName = item.PublisherName, labels=new "['Red', 'Blue', 'Yellow', 'Green', 'Purple', 'Orange']"

            //    ,
            //        backgroundColor= "rgba(75, 192, 192, 0.2)" ,borderWidth=1};

               
            //   // TempData["data"] = JsonConvert.SerializeObject(Publisherviewmodel1);

            //    Publisherviewmodels.Add(Publisherviewmodel1);
            //}


            return Json(Publisherviewmodels);
        }

        //public async Task<IEnumerable<Publisher>> GetPublish()
        //{
        //    var publisher =await _UW.Baserepository<ProjectBookShop.Models.Publisher>().FindAllAsync();
        //    return   Ok(publisher);
        //}
        public async Task<JsonResult> GetChartData()
        {

            List<Publisherviewmodel> Publisherviewmodels = new List<Publisherviewmodel>();
            var publisher = await _UW.Baserepository<ProjectBookShop.Models.Publisher>().FindAllAsync();
            var publisherT = publisher.Take(2);
            foreach (var item in publisherT)
            {
              
                Publisherviewmodel Publisherviewmodel1 = new Publisherviewmodel()
                {
                  //  PublisherID = item.PublisherID,
                    //PublisherName = item.PublisherName,
                    labels = new[] { "January", "February", "March", "April", "May", "June" },

                    datasets = new[]
                    {
                        new datasets
                        {

                    label = "Monthly Sales",
                    backgroundColor = "rgba(75, 192, 192, 0.2)",
                    borderColor = "rgba(75, 192, 192, 1)",
                    borderWidth = 1,
                    data = new[] { 65, 59, 80, 81, 56, 55 }
                        }
                    }
                };
                Publisherviewmodels.Add(Publisherviewmodel1);
            }
            var data1 = new
            {
                labels = new[] { "January", "February", "March", "April", "May", "June" },
                datasets = new[]
                {
                new
                {
                    label = "Monthly Sales",
                    backgroundColor = "rgba(75, 192, 192, 0.2)",
                    borderColor = "rgba(75, 192, 192, 1)",
                    borderWidth = 1,
                    data = new[] { 65, 59, 80, 81, 56, 55 }
                }
            }
            };
            
            
            return Json(Publisherviewmodels);
        }
    }
}
