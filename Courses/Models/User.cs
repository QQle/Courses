using Microsoft.AspNetCore.Identity;

namespace Courses.Models
{
    public class User : IdentityUser
    {
        public string? RefreshToken { get; set; }
        public DateTime? ExpiresDate { get; set; }
    }
}
