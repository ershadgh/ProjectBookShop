using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectBookShop.Models;
using ProjectBookShop.Models.ViewModels;
using System.Linq;
using ProjectBookShop.Models.Repository;
using System.Security.Policy;
using Microsoft.EntityFrameworkCore;
using ReflectionIT.Mvc.Paging;
using ProjectBookShop.Areas.Admin.Controllers;
using NuGet.Configuration;
using NuGet.Versioning;
using ProjectBookShop.Mapping;
using ProjectBookShop.Models.UnitOfWork;
using Newtonsoft.Json.Linq;
using System.Globalization;
using Newtonsoft.Json;
using Humanizer;

namespace ProjectBookShop.Areas.Admin
{
    [Area("Admin")]
    // [Route("admin")]
    public class BookController : Controller
    {
        /// <summary>
        /// private readonly BookShopContext _context;
        /// </summary>

        private readonly IUnitOfWork _UW;
        public BookController(IUnitOfWork UW)
        {


            _UW = UW;
        }

        public IActionResult Index(string Msg, int pageIndex = 1, int row = 5, String sortExpression = "Title", string title = "")
        {
            if (Msg != null)
            {
                ViewBag.Msg = $"{Msg}در ثبت اطلاعات خطا رخ داده است لطفا مجدد تلاش کنید!!! ";
            }

            title = string.IsNullOrEmpty(title) ? "" : title;
            List<int> Row = new List<int>
            {
                5,10,15,20,50,100
            };

            ViewBag.RowID = new SelectList(Row, row);
            ViewBag.Seaerch = title;
            ViewBag.NumOfPage = (pageIndex - 1) * row + 1;

            var PagingModel = PagingList.Create(_UW.BookRepositoryy().GetAllBook(title, "", "", "", "", "", ""), row, pageIndex, sortExpression, "Title");
            PagingModel.RouteValue = new RouteValueDictionary
            {
                {"row",row },
                {"title",title }
            };
            ViewBag.Categories = _UW.BookRepositoryy().GetAllCategories(null);
            ViewBag.LanguageID = new SelectList(_UW.Baserepository<Language>().FindAll(), "LanguageName", "LanguageName");
            ViewBag.PublisherID = new SelectList(_UW.Baserepository<ProjectBookShop.Models.Publisher>().FindAll(), "PublisherName", "PublisherName");
            ViewBag.AuthorID = new SelectList(_UW.Baserepository<Author>().FindAll().Select(t => new AuthorList { AuthorID = t.AuthorID, NameFamily = t.FirstName + " " + t.LastName }), "NameFamily", "NameFamily");
            ViewBag.TranslatorID = new SelectList(_UW.Baserepository<Translator>().FindAll().Select(t => new TranslatorList { TranslatorID = t.TranslatorID, NameFamily = t.Name + " " + t.Family }), "NameFamily", "NameFamily");


            return _UW.BookRepositoryy().GetAllBook(title, "", "", "", "", "", "") != null ? View(PagingModel) : Problem("Entity set 'BookShopContext.Books'  is null.");
        }
        [HttpPost]
        public IActionResult Advancedsearch(BooksAdvancedSearch ViewModel)
        {
            ViewModel.Title = string.IsNullOrEmpty(ViewModel.Title) ? "" : ViewModel.Title;
            ViewModel.ISBN = string.IsNullOrEmpty(ViewModel.ISBN) ? "" : ViewModel.ISBN;
            ViewModel.Language = string.IsNullOrEmpty(ViewModel.Language) ? "" : ViewModel.Language;
            ViewModel.PublusherName = string.IsNullOrEmpty(ViewModel.PublusherName) ? "" : ViewModel.PublusherName;
            ViewModel.Author = string.IsNullOrEmpty(ViewModel.Author) ? "" : ViewModel.Author;
            ViewModel.Translatorr = string.IsNullOrEmpty(ViewModel.Translatorr) ? "" : ViewModel.Translatorr;
            ViewModel.category = string.IsNullOrEmpty(ViewModel.category) ? "" : ViewModel.category;
            var Books = _UW.BookRepositoryy().GetAllBook(ViewModel.Title, ViewModel.ISBN, ViewModel.Language, ViewModel.PublusherName, ViewModel.Author, ViewModel.Translatorr, ViewModel.category);
            return View(Books);
        }
        [HttpGet]
        public IActionResult Create()
        {



            ViewBag.LanguageID = new SelectList(_UW.Baserepository<Language>().FindAll(), "LanguageID", "LanguageName");
            ViewBag.PublisherID = new SelectList(_UW.Baserepository<ProjectBookShop.Models.Publisher>().FindAll(), "PublisherID", "PublisherName");
            ViewBag.AuthorID = new SelectList(_UW.Baserepository<Author>().FindAll().Select(t => new AuthorList { AuthorID = t.AuthorID, NameFamily = t.FirstName + " " + t.LastName }), "AuthorID", "NameFamily");
            ViewBag.TranslatorID = new SelectList(_UW.Baserepository<Translator>().FindAll().Select(t => new TranslatorList { TranslatorID = t.TranslatorID, NameFamily = t.Family + " " + t.Name }), "TranslatorID", "NameFamily");
            BooksCreateEditViewModel ViewModel = new BooksCreateEditViewModel(_UW.BookRepositoryy().GetAllCategories(null));


            return View(ViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(BooksCreateEditViewModel booksCreate)
        {
            if (ModelState.IsValid)
            {
                if (await _UW.BookRepositoryy().CreateBookAsync(booksCreate))
                    return RedirectToAction("Index", new { Msg = "f" });
                else
                    ViewBag.Error = "در انجام عملیات خطایی رخ داده اشت";
            }


            ViewBag.LanguageID = new SelectList(_UW.Baserepository<Language>().FindAll(), "LanguageID", "LanguageName");
            ViewBag.PublisherID = new SelectList(_UW.Baserepository<ProjectBookShop.Models.Publisher>().FindAll(), "PublisherID", "PublisherName");
            ViewBag.AuthorID = new SelectList(_UW.Baserepository<Author>().FindAll().Select(t => new AuthorList { AuthorID = t.AuthorID, NameFamily = t.FirstName + " " + t.LastName }), "AuthorID", "NameFamily");
            ViewBag.TranslatorID = new SelectList(_UW.Baserepository<Translator>().FindAll().Select(t => new TranslatorList { TranslatorID = t.TranslatorID, NameFamily = t.Family + " " + t.Name }), "TranslatorID", "NameFamily");
            booksCreate.Categories = _UW.BookRepositoryy().GetAllCategories(null);
            return View(booksCreate);
        }

        public IActionResult Details(int id)
        {

            //var BookInfo = _context.ReadAllBooks.FromSqlRaw("SELECT b.BookID, b.ISBN, b.Imagee, b.IsPublishe, b.NumOfPages, b.Price, b.PublisheDate, b.PublisheYear, b.Stock, b.Summery, b.Title, b.weight, l.LanguageName, p.PublisherName, dbo.GetAllAuthor(b.BookID) AS Authors,  dbo.GetAllTranstors(b.BookID) AS Transslators, dbo.GetAllCategorise(b.BookID) AS Categories FROM  dbo.BookInfo AS b INNER JOIN  dbo.Languages AS l ON b.LanguageID = l.LanguageID INNER JOIN   dbo.Publisher AS p ON p.PublisherID = b.PublisherID WHERE(b.DeleteBook = 0)").Where(b => b.BookID == id).First();
            //نحوه استفاده از view   در .net core08
            var BookInfo = _UW._context.ReadAllBooks.Where(b => b.BookID == id).First();
            //نحوه ی استفاده از دستورات اس کیو ال rowsqlquery
            ///var BookInfo = _context.Books.FromSql($"select * from dbo.BookInfo where BookID={id}").Include(a=>a.Language).Include(a=>a.Publisher).First();
            //var BookInfo = _context.ReadAllBooks.Where(b => b.BookID == id).First();
            //var b = _context.ReadAllBooks.ToList();

            //نحوه استفاده از پروسیجر در .net core
            //ViewBag.Autors = _context.Authors.FromSql($"EXECUTE GetAuthorsByBookID {id}").ToList();
            //نحوه ی استفاده از دستورات اس کیو ال rowsqlquery
            //ViewBag.Translate = _context.Translator.FromSql($"select T.TranslatorID , T.Name,T.Family from dbo.Translator as T join dbo.Book_Translators as TB " + $" on Tb.TranslatorID=t.TranslatorID where TB.BookID={id}").ToList();
            //ViewBag.Categories = (from c in _context.Book_Ctegory.Include(a => a.Category) where (c.BookID == id) select new Category { CategoryName = c.Category.CategoryName,CategoryID=c.CategoryID }).ToList();
            var b = _UW._context.ReadAllBooks.FromSql($"select * from ReadAllBooks where BookID={id}").First();


            return View(BookInfo);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _UW.Baserepository<Book>() == null)
            {
                return NotFound();
            }
            var Book = await _UW.Baserepository<Book>().FindbyIdAsync(id);
            if (Book == null)
            {
                return NotFound();
            }
            else
            {
                var ViewModel = await (from b in _UW._context.Books.Include(l => l.Language).Include(p => p.Publisher)
                                       where (b.BookID == id)
                                       select new BooksCreateEditViewModel
                                       {
                                           BookId = b.BookID,
                                           Title = b.Title,
                                           Summary = b.Summery,
                                           Price = b.Price,
                                           Stock = b.Stock,
                                           ImageBase64 = b.Fille,
                                           NumOfPages = b.NumOfPages,
                                           Weight = b.weight,
                                           ISBN = b.ISBN,
                                           IsPublish = b.IsPublishe,
                                           PublisheYear = b.PublisheYear,
                                           LanguageID = b.LanguageID,
                                           PublisherID = b.PublisherID,

                                       }).FirstAsync();
                int[] AuthorArray = await (from a in _UW._context.Author_Books
                                           where (a.BookID == id)
                                           select a.AuthorID).ToArrayAsync();
                int[] TranslatorArray = await (from t in _UW._context.Book_Translators
                                               where (t.BookID == id)
                                               select t.TranslatorID).ToArrayAsync();
                int[] CatagoryArray = await (from c in _UW._context.Book_Ctegory
                                             where (c.BookID == id)
                                             select c.CategoryID).ToArrayAsync();
                ViewModel.AuthorID = AuthorArray;
                ViewModel.TranslatorID = TranslatorArray;
                ViewModel.CategoryID = CatagoryArray;
                ViewBag.LanguageID = new SelectList(_UW.Baserepository<Language>().FindAll(), "LanguageID", "LanguageName");
                ViewBag.PublisherID = new SelectList(_UW.Baserepository<Models.Publisher>().FindAll(), "PublisherID", "PublisherName");
                ViewBag.AuthorID = new SelectList(_UW.Baserepository<Author>().FindAll().Select(t => new AuthorList { AuthorID = t.AuthorID, NameFamily = t.FirstName + " " + t.LastName }), "AuthorID", "NameFamily");
                ViewBag.TranslatorID = new SelectList(_UW.Baserepository<Translator>().FindAll().Select(t => new TranslatorList { TranslatorID = t.TranslatorID, NameFamily = t.Name + " " + t.Family }), "TranslatorID", "NameFamily");
                //TreeViewCategory treeViewCategory = new TreeViewCategory(CatagoryArray);
                ViewModel.Categories = _UW.BookRepositoryy().GetAllCategories(CatagoryArray);


                return View(ViewModel);
            }

        }
        [HttpPost]
        public async Task<IActionResult> Edit(BooksCreateEditViewModel viewModel)
        {
            ViewBag.LanguageID = new SelectList(_UW.Baserepository<Language>().FindAll(), "LanguageID", "LanguageName");
            ViewBag.PublisherID = new SelectList(_UW.Baserepository<Models.Publisher>().FindAll(), "PublisherID", "PublisherName");
            ViewBag.AuthorID = new SelectList(_UW.Baserepository<Author>().FindAll().Select(t => new AuthorList { AuthorID = t.AuthorID, NameFamily = t.FirstName + " " + t.LastName }), "AuthorID", "NameFamily");
            ViewBag.TranslatorID = new SelectList(_UW.Baserepository<Translator>().FindAll().Select(t => new TranslatorList { TranslatorID = t.TranslatorID, NameFamily = t.Name + " " + t.Family }), "TranslatorID", "NameFamily");
            //TreeViewCategory treeViewCategory = new TreeViewCategory(CatagoryArray);
            viewModel.Categories = _UW.BookRepositoryy().GetAllCategories(viewModel.CategoryID);

            if (ModelState.IsValid)
            {
                if (await _UW.BookRepositoryy().EditBookAsync(viewModel))
                {
                    ViewBag.MsgSucces = "تغییرات با موفقیت انجام شد";
                    return View(viewModel);

                }
                else

                {
                    ViewBag.MsgFaild = "در ذخیره تغییرات خطایی رخ داده است";
                    return View(viewModel);
                }
            }

            else
            {
                ViewBag.MsgFaild = "اطلاعات فرم نامعتبر است";
                return View(viewModel);
            }
        }





        public async Task<IActionResult> Delete(int id)
        {
            var Book = await _UW.Baserepository<Book>().FindbyIdAsync(id);

            if (Book != null)
            {

                Book.DeleteBook = true;
                /// _context.Update(Book);
                await _UW.commit();
            }
            else
            {
                return NotFound();
            }


            // var BookInfo = (from b in _context.Books.Where(b => b.BookID == id) select new BooksIndexViewModel { }.First());
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> SearchByIsbn(string ISBN)
        {
            if (ISBN != null)
            {
                var Book = (from b in _UW._context.Books
                            where (b.ISBN == ISBN)
                            select new BooksIndexViewModel
                            {
                                Title = b.Title,
                                Author = BookShopContext.GetAllAuthor(b.BookID),
                                Translatorr = BookShopContext.GetAllTranstors(b.BookID),
                                category = BookShopContext.GetAllCategorise(b.BookID),
                            }).FirstOrDefaultAsync();
                if (Book.Result == null)
                {
                    ViewBag.Msg = "کتابی با این شابک پیدا نشد";
                    //return View();
                }
                return View(await Book);
            }
            else
            {
                return View();
            }

        }
       
        public async Task<IActionResult> Buy(int[] selectedIds)
        {
           var m=await _UW.BookRepositoryy().BuyBook(selectedIds);
        
            return  View(m);
        }
    }

}
