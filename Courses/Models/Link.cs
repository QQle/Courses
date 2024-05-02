using System.ComponentModel.DataAnnotations;

namespace Courses.Models
{
    public class Link
    {
        [Key]
        public int LinkID { get; set; }
        public string Url { get; set; } 
        public int CourseID { get; set; } 
        public Cours Course { get; set; }
    }
}
