using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Courses.Models
{
    public class UsersCourses
    {
        [Key]
        public int CourseID { get; set; }
        public User User { get; set; }
        public Cours Cours { get; set; }
        
    }
}
