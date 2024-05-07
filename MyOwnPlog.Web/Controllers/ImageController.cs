using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MyOwnPlog.Web.Repositories;
using System.Net;

namespace MyOwnPlog.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IImageRepository imageRepository;

        public ImageController(IImageRepository imageRepository)
        {
            this.imageRepository = imageRepository;
        }

        [HttpGet]
        public IActionResult Get()
        {

            return Ok("this is Api Image Controller");
        }

        [HttpPost]

        public async Task <IActionResult> UploadAsync(IFormFile file) 
        
        { 
           var imageUrl = await imageRepository.UploadAsync(file);

            if(imageUrl == null)
            {

                return Problem("Something want wrong", null, (int)HttpStatusCode.InternalServerError);
            }

            return new JsonResult(new { link = imageUrl });
        }
    }
}
