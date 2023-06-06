namespace FreeCourse.Web.Models
{
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }

        public IEnumerable<string> GetUserProperties()
        {
            yield return UserName;
            yield return Email;
            yield return City;
        }
    }
}
