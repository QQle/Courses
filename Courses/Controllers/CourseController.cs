using Courses.Contexts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses.Controllers
{
    [ApiController]
    [Route("[controller]")]

    public class CourseController : Controller
    {
        private readonly CourseContext _context;
        public CourseController(CourseContext context)
        {
            _context = context;
        }

        [HttpGet("GetAllCourses")]
        public async Task<IActionResult> GetCourse() 
        {
           var result = await _context.CoursesItem
                .ToListAsync();

            if (result.Count == 0)
            {
                BadRequest("Доступных курсов нет");
            }
            return Ok(result);
           
        
        }

        [HttpPost("FindTaskByID")]
        public async Task<IActionResult> FindTaskByID([FromBody] int id)
        {
            var result = await _context.Tasks
                .Where(x=>x.TaskId == id)
                .ToListAsync();

            if (result.Count == 0)
            {
                BadRequest("Задание по введенному Id не найдено");
            }
            return Ok(result);
        }

        [HttpGet("GetAllResults")]
        public async Task<IActionResult> GetAllResults()
        {
            var result = await _context.CourseResults
                .ToListAsync();

            if (result.Count == 0)
            {
                BadRequest("Нет результатов");
            }
            return Ok(result);

        }

        public record InsertResults();
       
    }
}
