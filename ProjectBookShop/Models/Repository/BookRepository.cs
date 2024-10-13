using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using NuGet.Protocol.Core.Types;
using ProjectBookShop.classes;
using ProjectBookShop.Models.UnitOfWork;
using ProjectBookShop.Models.ViewModels;
using System.Linq;
using System.Web.WebPages.Html;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Microsoft.AspNetCore.Mvc.Filters;
namespace ProjectBookShop.Models.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookShopContext _context;
        private readonly IConvertDate _convertDate;
        private readonly IUnitOfWork _UW;
        public BookRepository(BookShopContext context,IConvertDate convertDate,IUnitOfWork UW)
        {
            _context = context;
            _convertDate = convertDate;
            _UW = UW;
        }
        public List<BooksIndexViewModel> GetAllBook(string title, string ISBN, string Language, string PublusherName, string Author, string Translator, string Category)
        {


            string AuthorsName = "";
            string TranslatorName = "";
            string CatagoryName = "";
            List<BooksIndexViewModel> viewModel = new List<BooksIndexViewModel>();
            //var Book = (from b in _context.Books
            //            join
            //            p in _context.Publisher on b.PublisherID equals p.PublisherID
            //            join l in _context.Languages on b.LanguageID equals l.LanguageID
            //            join AB in _context.Author_Books on b.BookID equals AB.BookID
            //            join A in _context.Authors on AB.AuthorID equals A.AuthorID
            //            join TB in _context.Book_Translators on b.BookID equals TB.BookID
            //            join T in _context.Translator on TB.TranslatorID equals T.TranslatorID
            //            where (b.Delete == false)
            //            select new BooksIndexViewModel
            //            {
            //                BookID = b.BookID,
            //                Title = b.Title,
            //                Price = b.Price,
            //                PublusherName = p.PublisherName,
            //                Ispublise = b.IsPublishe,
            //                PublisheDate = b.PublisheDate,
            //                Language = l.LanguageName,
            //                Author = A.FirstName + " " + A.LastName,
            //                Translatorr = T.Name + " " + T.Family
            //            })
            //            .GroupBy(b => b.BookID).Select(g => new { BookId = g.Key, BookGroups = g.ToList() }).ToList();


            var Books = (from AB in _context.Author_Books.Include(b => b.Book).ThenInclude(p => p.Publisher)
                         .Include(b => b.Book).ThenInclude(l => l.Language)
                        .Include(a => a.Author)
                         join S in _context.Book_Translators on AB.BookID equals S.BookID into bt
                         from bts in bt.DefaultIfEmpty()
                         join t in _context.Translator on bts.TranslatorID equals t.TranslatorID into tr
                         from trl in tr.DefaultIfEmpty()

                         join r in _context.Book_Ctegory on AB.Book.BookID equals r.BookID into bc
                         from bct in bc.DefaultIfEmpty()
                         join c in _context.Categories on bct.CategoryID equals c.CategoryID into cg
                         from cog in cg.DefaultIfEmpty()
                         where (/*AB.Book.DeleteBook == false &&*/ AB.Book.Title.Contains(title.TrimStart().TrimEnd()) && EF.Functions.Like(AB.Book.ISBN, "%" + ISBN + "%") &&
                          AB.Book.Language.LanguageName.Contains(Language.TrimStart().TrimEnd()) && EF.Functions.Like(AB.Book.Publisher.PublisherName, "%" + PublusherName + "%")

                           )
                         select new
                         {
                             AB.Book.BookID,
                             AB.Book.Title,
                             AB.Book.Price,
                             PublusherName = AB.Book.Publisher.PublisherName,
                             AB.Book.IsPublishe,
                             AB.Book.PublisheDate,
                             Language = AB.Book.Language.LanguageName,
                             Author = AB.Author.FirstName + " " + AB.Author.LastName,
                             Translatorr = bts != null ? trl.Name + " " + trl.Family : "",
                             category = bct != null ? cog.CategoryName : "",

                              AB.Book.ISBN
                         }).Where(A => A.Author.Contains(Author) && A.Translatorr.Contains(Translator) && A.category.Contains(Category)).
                        GroupBy(b => b.BookID).Select(g => new { BookId = g.Key, BookGroups = g.ToList() }).AsNoTracking() /*.IgnoreQueryFilters()*/.ToList();
            foreach (var item in Books)
            {
                AuthorsName = "";
                TranslatorName = "";
                foreach (var groups in item.BookGroups.Select(a => a.Author).Distinct())
                {
                    if (AuthorsName == "")
                        AuthorsName = groups;
                    else
                        AuthorsName = AuthorsName + "-" + groups;
                }

                foreach (var groups in item.BookGroups.Select(a => a.Translatorr).Distinct())
                {
                    if (TranslatorName == "")
                        TranslatorName = groups;
                    else
                        TranslatorName = TranslatorName + "-" + groups;

                }
                foreach (var groups in item.BookGroups.Select(a => a.category).Distinct())
                {
                    if (CatagoryName == "")
                        CatagoryName = groups;
                    else
                        CatagoryName = CatagoryName + "-" + groups;
                }
                BooksIndexViewModel VM = new BooksIndexViewModel()
                {
                    Author = AuthorsName,
                    BookID = item.BookId,
                    Title = item.BookGroups.First().Title,
                    ISBN = item.BookGroups.First().ISBN,
                    Price = item.BookGroups.First().Price,
                    PublusherName = item.BookGroups.First().PublusherName,
                    Ispublise = item.BookGroups.First().IsPublishe == true ? "منتشر شده" : "پیش نویس",
                    PublisheDate = item.BookGroups.First().PublisheDate!=null?_convertDate.ConverMiladitoShamsi((DateTime)item.BookGroups.First().PublisheDate, "yyyy/MM/DD"):null,
                    Language = item.BookGroups.First().Language,
                    Translatorr = TranslatorName,
                    category = CatagoryName,

                };
                viewModel.Add(VM);

            }
            return viewModel;
        }
        public List<TreeViewCategory> GetAllCategories(int[]? catagoryArayy)
        {
            List<TreeViewCategory> Categorie = (from c in _context.Categories
                                                where (c.ParentCategoryID == null)
                                                select new TreeViewCategory { id = c.CategoryID, title = c.CategoryName, CategoryID = catagoryArayy }).ToList();
            foreach (var item in Categorie)
            {
                BindSubCategories(item);
            }
            return Categorie;
        }
        public void BindSubCategories(TreeViewCategory category)
        {
            var SubCaegories = (from c in _context.Categories
                                where (c.ParentCategoryID == category.id)
                                select new TreeViewCategory
                                {
                                    title = c.CategoryName,
                                    CategoryID = category.CategoryID,
                                    id = c.CategoryID
                                }).ToList();
            foreach (var item in SubCaegories)
            {
                BindSubCategories(item);
                //if (category.SubCategorice!=null)
                category.subs.Add(item);




            }
        }
        public List<Language> getlanguage()
        {
            var lstlan = _context.Languages.ToList();
            return lstlan;

        }

        public async Task<bool> CreateBookAsync(BooksCreateEditViewModel booksCreate)
        {
            try

            {
                byte[] Image = null;
                if (!string.IsNullOrWhiteSpace(booksCreate.ImageBase64))
                {
                    Image = Convert.FromBase64String(booksCreate.ImageBase64);
                }
                List<Book_Translator> translations = new List<Book_Translator>();
                List<Book_Ctegory> ctegories = new List<Book_Ctegory>();

                if (booksCreate.TranslatorID != null)
                    translations = booksCreate.TranslatorID.Select(a => new Book_Translator { TranslatorID = a }).ToList();
                if (booksCreate.CategoryID != null)
                    ctegories = booksCreate.CategoryID.Select(c => new Book_Ctegory { CategoryID = c }).ToList();

                var Transaction = _UW._context.Database.BeginTransaction();



                //authors = booksCreate.AuthorID.Select(a => new Author_Book { AuthorID = a }).ToList();
                DateTime PublisheDate = booksCreate.IsPublish == true ? DateTime.Now : DateTime.MinValue;


                Book book = new Book()
                {

                    Title = booksCreate.Title,
                    Summery = booksCreate.Summary,
                    Price = Convert.ToDecimal(booksCreate.Price),
                    Stock = booksCreate.Stock,
                    Fille = booksCreate.ImageBase64,
                    Imagee = Image,
                    NumOfPages = booksCreate.NumOfPages,
                    weight = booksCreate.Weight,
                    ISBN = booksCreate.ISBN,
                    IsPublishe = booksCreate.IsPublish,
                    PublisheYear = booksCreate.PublisheYear,
                    LanguageID = booksCreate.LanguageID,
                    PublisherID = booksCreate.PublisherID,
                    //PublisheDate = PublisheDate,
                    Author_Books = booksCreate.AuthorID.Select(a => new Author_Book { AuthorID = a }).ToList(),
                    Book_Ctegories = ctegories,
                    book_Tranlators = translations,

                    //  DeleteBook = false
                };

                await _UW.Baserepository<Book>().Create(book);
                //await _context.SaveChangesAsync();

                //if (booksCreate.TranslatorID != null)
                //{
                //    for (int i = 0; i < booksCreate.TranslatorID.Length; i++)
                //    {
                //        Book_Translator translator = new Book_Translator()
                //        {
                //            BookID = book.BookID,
                //            TranslatorID = booksCreate.TranslatorID[i]
                //        };
                //        translations.Add(translator);
                //    }
                //    await _context.Book_Translators.AddRangeAsync(translations);
                //    await _context.SaveChangesAsync();
                //}





                //for (int i = 0; i < booksCreate.AuthorID.Length; i++)
                //{
                //    Author_Book author = new Author_Book()
                //    {
                //        BookID = book.BookID,
                //        AuthorID = booksCreate.AuthorID[i]
                //    };
                //    authors.Add(author);

                //}
                //await _context.Author_Books.AddRangeAsync(authors);
                //await _context.SaveChangesAsync();


                //if (booksCreate.CategoryID != null)
                //{
                //    for (int i = 0; i < booksCreate.CategoryID.Length; i++)
                //    {
                //        Book_Ctegory ctegory = new Book_Ctegory()
                //        {
                //            BookID = book.BookID,
                //            CategoryID = booksCreate.CategoryID[i]
                //        };
                //        ctegories.Add(ctegory);
                //    }
                //    await _context.Book_Ctegory.AddRangeAsync(ctegories);



                //}
                await _UW.commit();
                Transaction.Commit();
                return true;

            }catch(Exception e)
            {
                return false;
            }





        }
        public async Task<bool> EditBookAsync(BooksCreateEditViewModel viewModel)
         {
            try
            {
                Book book = new Book()
                {
                    BookID = viewModel.BookId,
                    Title = viewModel.Title,
                    Summery = viewModel.Summary,
                    Price = Convert.ToDecimal(viewModel.Price),
                    Stock = viewModel.Stock,
                    Fille = viewModel.ImageBase64,
                    NumOfPages = viewModel.NumOfPages,
                    weight = viewModel.Weight,
                    ISBN = viewModel.ISBN,
                    IsPublishe = viewModel.IsPublish,
                    PublisheYear = viewModel.PublisheYear,
                    LanguageID = viewModel.LanguageID,
                    PublisherID = viewModel.PublisherID,
                    DeleteBook = false

                };
                _UW.Baserepository<Book>().Update(book);
                await _UW.commit();

                var recentAuthor = (from a in _UW._context.Author_Books
                                    where (a.BookID == viewModel.BookId)
                                    select (a.AuthorID)).ToArray();

                var recentTransltor = (from t in _UW._context.Book_Translators
                                       where (t.BookID == viewModel.BookId)
                                       select (t.TranslatorID)).ToArray();
                var recentcategory = (from c in _UW._context.Book_Ctegory
                                      where (c.BookID == viewModel.BookId)
                                      select (c.CategoryID)).ToArray();

                var DeletedAuthors = recentAuthor.Except(viewModel.AuthorID);

                int[] DeletedTranslator = null;
                if (recentTransltor.Count() != 0)
                {
                    DeletedTranslator = recentTransltor.Except(viewModel.TranslatorID).ToArray();
                }
                int[] DeletedCategory = null;
                if (recentcategory.Count() != 0 && viewModel.CategoryID != null)
                {
                    DeletedCategory = recentcategory.Except(viewModel.CategoryID).ToArray();
                }

                var AddedAuthor = viewModel.AuthorID.Except(recentAuthor);
                int[] AddedTranslator = null;
                if (viewModel.TranslatorID != null)
                {
                    AddedTranslator = viewModel.TranslatorID.Except(recentTransltor).ToArray();
                }
                int[] AddedCategory = null;
                if (viewModel.CategoryID != null)
                {
                    AddedCategory = viewModel.CategoryID.Except(recentcategory).ToArray();
                }

                if (DeletedAuthors.Count() != 0)
                {
                    _UW.Baserepository<Author_Book>().DeletedRange(DeletedAuthors.Select(a => new Author_Book { AuthorID = a, BookID = viewModel.BookId }).ToList());
                    await _UW.commit();
                }

                if (DeletedTranslator != null)
                {

                    _UW.Baserepository<Book_Translator>().DeletedRange(DeletedTranslator.Select(t => new Book_Translator { TranslatorID = t, BookID = viewModel.BookId }).ToList());
                    await _UW.commit();
                }

                if (DeletedCategory != null)
                {
                    _UW.Baserepository<Book_Ctegory>().DeletedRange(DeletedCategory.Select(c => new Book_Ctegory { CategoryID = c, BookID = viewModel.BookId }).ToList());
                    await _UW.commit();
                }


                if (AddedAuthor.Count() != 0)
                {
                    _UW.Baserepository<Author_Book>().CreateRange(AddedAuthor.Select(a => new Author_Book { AuthorID = a, BookID = viewModel.BookId }).ToList());
                    await _UW.commit();
                }

                if (AddedTranslator != null)
                {
                    _UW.Baserepository<Book_Translator>().CreateRange(AddedTranslator.Select(t => new Book_Translator { TranslatorID = t, BookID = viewModel.BookId }).ToList());
                    await _UW.commit();
                }

                if (AddedCategory != null)
                {
                    _UW.Baserepository<Book_Ctegory>().CreateRange(AddedCategory.Select(c => new Book_Ctegory { CategoryID = c, BookID = viewModel.BookId }).ToList());
                    await _UW.commit();
                }
             
                return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        public async Task<List<ProductsViewModel>> BuyBook(int[] ids)
        {
            var  books = new List<ProductsViewModel>();
            if (ids.Any())
            {
                 books = _context.Books.Where(i => ids.Contains(i.BookID) ).Select(i => new ViewModels.ProductsViewModel { ProductId = i.BookID, ProductName = i.Title, Price = i.Price }).ToList();
                var Total = books.Sum(p => p.Price);
                foreach (var item in books)
                {
                    
                    item.TotalPrice = Total.ToString("0");
                }

                return  books;
            }
            else
            {
                return null;
            }
            
           
        }
    }
}
