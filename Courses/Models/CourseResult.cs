using System.ComponentModel.DataAnnotations;

namespace Courses.Models
{
    public class CourseResult
    {
        [Key]
        public int ResultId { get; set; }
        public int Score { get; set; }
        public DateTime TestDate { get; set; }
        public UsersCourses UsersCourses { get; set; }
    }
}
