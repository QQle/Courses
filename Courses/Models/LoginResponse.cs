namespace Courses.Models
{
    public class LoginResponse
    {
        public bool IsLogged { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
