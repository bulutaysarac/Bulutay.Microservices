using FreeCourse.IdentityServer.Dtos;
using FreeCourse.IdentityServer.Models;
using FreeCourse.Shared.Dtos;
using IdentityServer4;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.IdentityServer.Controllers
{
    [Authorize(IdentityServerConstants.LocalApi.PolicyName)]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public AuthController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(SignUpDto signUpDto)
        {
            var user = new ApplicationUser
            {
                UserName = signUpDto.Username,
                Email = signUpDto.Email,
                City = signUpDto.City
            };

            var result = await _userManager.CreateAsync(user, signUpDto.Password);
            if(!result.Succeeded)
            {
                return BadRequest(Response<NoContent>.Fail(result.Errors.Select(x => x.Description).ToList(), 400));
            }
            return Ok();
        }

        [HttpGet]
        public async Task<IActionResult> GetUser()
        {
            var userClaims = User.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Sub);
            if(userClaims == null)
            {
                return BadRequest();
            }
            var user = await _userManager.FindByIdAsync(userClaims.Value);
            if(user == null)
            {
                return BadRequest();
            }

            return Ok(new
            {
                user.Id,
                user.UserName,
                user.Email,
                user.City
            });
        }
    }
}
