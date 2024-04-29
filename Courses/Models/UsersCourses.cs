using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Courses.Models
{
    public class UsersCourses
    {
        [Key]
        public int UserCourseID { get; set; }
        public string UserId { get; set; }
        public Cours Cours { get; set; }
        
    }
}
