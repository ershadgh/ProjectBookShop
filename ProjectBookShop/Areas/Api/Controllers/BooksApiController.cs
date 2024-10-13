using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectBookShop.Areas.Api.Classes;
using ProjectBookShop.Models.UnitOfWork;
using ProjectBookShop.Models.ViewModels;

namespace ProjectBookShop.Areas.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [ApiResultFilter]
    public class BooksApiController : ControllerBase
    {
        readonly private IUnitOfWork _UW;
        public BooksApiController(IUnitOfWork UW)
        {
            _UW = UW;
        }
        [HttpGet("[action]")]
        public ApiResult<List<BooksIndexViewModel>> GetBooks()
        {
                return Ok(_UW.BookRepositoryy().GetAllBook("", "", "", "", "", "", ""));
            
        }
        [HttpPost]
        public async Task<ApiResult> CreateBookAsync(BooksCreateEditViewModel booksCreate)
        {

            if (await _UW.BookRepositoryy().CreateBookAsync(booksCreate))
            {
                return Ok();
            }
            else
            {
                return BadRequest("در انجام عملیات خطای رخ داده است");
            }
        }
            [HttpPut]
            public async Task<ApiResult> EditBook(BooksCreateEditViewModel ViewModel)
            {
            if (await _UW.BookRepositoryy().EditBookAsync(ViewModel))
                return Ok();
            else
                 return BadRequest( " در انجام عملیات خطای رخ داده است");
               
            }
            [HttpDelete("{id}")]
            public async Task<IActionResult> DeletBook(int id)
            {

                var Book = await _UW.Baserepository<Book>().FindbyIdAsync(id);

                if (Book != null)
                {

                    Book.DeleteBook = true;
                    /// _context.Update(Book);
                    await _UW.commit();
                // return Content("حذف اطلاعات با موفقیت انجام شد");
                return Ok();
                }
                else
                {
                    return NotFound();
                }


                // var BookInfo = (from b in _context.Books.Where(b => b.BookID == id) select new BooksIndexViewModel { }.First());

            }
        }
    }
