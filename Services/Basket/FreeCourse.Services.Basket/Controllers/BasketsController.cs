using FreeCourse.Services.Basket.Dtos;
using FreeCourse.Services.Basket.Services;
using FreeCourse.Shared.Extensions;
using FreeCourse.Shared.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.Basket.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketsController : ControllerBase
    {
        private readonly IBasketService _basketService;
        private readonly ISharedIdentityService _sharedIdentityService;

        public BasketsController(IBasketService basketService, ISharedIdentityService sharedIdentityService)
        {
            _basketService = basketService;
            _sharedIdentityService = sharedIdentityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetBasket()
        {
            var response = await _basketService.GetBasket(_sharedIdentityService.GetUserId);
            return this.CreateRespose(response);
        }

        [HttpPost]
        public async Task<IActionResult> SaveOrUpdateBasket(BasketDto basketDto)
        {
            var response = await _basketService.SaveOrUpdate(basketDto);
            return this.CreateRespose(response);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteBasket()
        {
            var response = await _basketService.Delete(_sharedIdentityService.GetUserId);
            return this.CreateRespose(response);
        }
    }
}
