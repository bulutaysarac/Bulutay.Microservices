using FreeCourse.Shared.Dtos;
using FreeCourse.Shared.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.FakePayment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FakePaymentsController : ControllerBase
    {
        [HttpPost]
        public IActionResult ReceivePayment()
        {
            return this.CreateRespose(Response<NoContent>.Success(200));
        }
    }
}
