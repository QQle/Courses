using System.ComponentModel.DataAnnotations;

namespace Courses.Models
{
    public class TaskAnswer
    {
        [Key]
        public int ResultID { get; set; }
        public int Status { get; set; }
        public string Answer { get; set; }
        public Task Task { get; set; }
    }
}
