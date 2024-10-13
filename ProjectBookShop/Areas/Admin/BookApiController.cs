using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectBookShop.Models.Repository;

namespace ProjectBookShop.Areas.Admin
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookApiController : ControllerBase
    {
        private readonly BookRepository _bookRepository;

        public BookApiController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }

        [HttpGet("GetAllBook")]
        public IActionResult GetAllBook()
        {
          var langvage=  _bookRepository.getlanguage();
            return Ok( langvage);
        }
    }
}
