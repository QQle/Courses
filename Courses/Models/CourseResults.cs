using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Courses.Models
{
    public class CourseResults
    {
        [Key]
        public int ResultId { get; set; }
        public int Score { get; set; }
        public DateTime TestDate { get; set; }

        [ForeignKey("UserCourseId")]
        public int UserCourseId { get; set; }
       
       
    }
}
