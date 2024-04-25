using System.ComponentModel.DataAnnotations;

namespace Courses.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        public string Description { get; set; }
        public TaskAnswer Answers { get; set; }
        public Cours CoursId { get; set; }

    }
}
