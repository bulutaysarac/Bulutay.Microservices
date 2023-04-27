using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Shared.Extensions
{
    public static class ControllerExtensions
    {
        public static IActionResult CreateRespose<T>(this ControllerBase controller, Response<T> response)
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
