using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotosController : ControllerBase
    {

        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo, CancellationToken cancellationToken)
        {
            if(photo != null)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await photo.CopyToAsync(stream, cancellationToken);
                }
                var returnPath = "photos/" + photo.FileName;
                PhotoDto photoDto = new PhotoDto()
                {
                    Url = returnPath
                };
                return this.CreateRespose(Response<PhotoDto>.Success(photoDto, 200));
            }
            return this.CreateRespose(Response<PhotoDto>.Fail("Photo is empty!", 400));
        }

        [HttpGet]
        public async Task<IActionResult> PhotoDelete(string photoUrl)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photoUrl);
            if(!System.IO.File.Exists(path))
            {
                return this.CreateRespose(Response<NoContent>.Fail("Photo not found!", 404));
            }
            System.IO.File.Delete(path);
            return this.CreateRespose(Response<NoContent>.Success(204));
        }
    }
}
