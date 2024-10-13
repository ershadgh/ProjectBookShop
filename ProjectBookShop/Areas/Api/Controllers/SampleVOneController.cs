using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ProjectBookShop.Areas.Api.Controllers
{
    //[Route("api/v{v:apiVersion}/[controller]")]
    //[Route("api/v{apiVersion}/[controller]")]
    [Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class SampleVOneController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1 from version1", "valuse2 from version2" };
        }
    }
}
