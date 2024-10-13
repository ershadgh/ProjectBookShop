using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace ProjectBookShop.Areas.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadFileController : ControllerBase
    {
        private readonly IHostingEnvironment _env;
        public UploadFileController(IHostingEnvironment env)
        {
            _env = env;
        }
        [HttpPost]
        public async Task<string> UploadImage([FromBody] string ImageBase64)
        {
            
            byte[] Bytes = Convert.FromBase64String(ImageBase64);
            var FilePath = Path.Combine($"{_env.WebRootPath}/Files/{Guid.NewGuid()}.jpg");
            await System.IO.File.WriteAllBytesAsync(FilePath, Bytes);
            return "آپلود عکس با موفقیت انجام شد";
        }

    }
}
