using System.ComponentModel.DataAnnotations;

namespace Courses.Models
{
    public class Cours
    {
        [Key]
        public int CourseID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public List<Task>  Tasks { get; set; }
        public Link Links { get; set; }

    }
}
