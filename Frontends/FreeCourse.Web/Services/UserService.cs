using FreeCourse.Web.Models;
using FreeCourse.Web.Services.Interfaces;

namespace FreeCourse.Web.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;

        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserModel> GetUser()
        {
            return await _httpClient.GetFromJsonAsync<UserModel>("/api/auth/getuser");
        }
    }
}
