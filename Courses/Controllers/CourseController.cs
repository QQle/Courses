using Courses.Contexts;
using Courses.Models;
using Microsoft.AspNetCore.Authorization;
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

        [HttpPost("SignUpCourse")]

        public async Task<IActionResult> SingUpCourse([FromBody] string userId, int courseId)
        {
            var user = await _context.Users.FindAsync(userId);
            var course = await _context.CoursesItem.FindAsync(courseId);

            if (user == null)
            {
                return NotFound($"User with ID {userId} not found.");
            }

            if (course == null)
            {
                return NotFound($"Course with ID {courseId} not found.");
            }

            var newUserCourse = new UsersCourses
            {
                UserId = userId,
                Cours = course
            };

            
            _context.UserCourses.Add(newUserCourse);

          
            await _context.SaveChangesAsync();

            return Ok(newUserCourse);
        }

       [HttpPost("GetTasksByCourseId")]
       public async Task<IActionResult> GetTask([FromBody]int courseId)
        {
            var courseWithTasks = _context.CoursesItem
             .Where(c => c.CourseID == courseId)
             .Select(c => new Cours
             {
                
                 Tasks = _context.Tasks
                     .Where(t => t.CoursId.CourseID == courseId)
                     .Select(t => new Models.Task
                     {
                         TaskId = t.TaskId,
                         Description = t.Description,
                         Answers = t.Answers,
                        
                        
                     })
                     .ToList()
             })
             .FirstOrDefault();

            return Ok(courseWithTasks);
       }
        
        public record SetResult(int Score, DateTime SolveDate, int UserCourseId);

        [HttpPost("SetScore")]
        public async Task<IActionResult> SetScore([FromBody] SetResult setResult)
        {
            
            var userCourse = await _context.UserCourses.FindAsync(setResult.UserCourseId);


            if (userCourse == null)
            {
                return NotFound($"Course with ID {userCourse} not found.");
            }

            var CourseResult = new CourseResults
            {
                Score = setResult.Score,
                TestDate = setResult.SolveDate,
                UserCourseId = setResult.UserCourseId,
          
            };


            _context.CourseResults.Add(CourseResult);


            await _context.SaveChangesAsync();

            return Ok(CourseResult);
        }

        [HttpPost("GetUserProgress")]
        public async Task<IActionResult> GetProgress([FromBody] string userId)
        {
            var userCourseId = await _context.UserCourses
                .Where(x=>x.UserId==userId)
                .Select(c=>c.UserCourseID)
                .FirstOrDefaultAsync();

            if (userCourseId == 0 )
            {
                return BadRequest("Данный пользователь не проходил тесты");
            }

            var result = await _context.CourseResults
                .Where(x=>x.UserCourseId==userCourseId)
                .ToListAsync(); 

            return Ok(result);
        }


        [HttpPost("GetUserProgressByDifferentCourse")]
        public async Task<IActionResult> GetProgressByCourse([FromBody] string userId)
        {
            var progress = await (from uc in _context.UserCourses
                                  join cr in _context.CourseResults on uc.UserCourseID equals cr.UserCourseId
                                  where uc.UserId == userId
                                  select new
                                  {
                                      CourseName = uc.Cours.Title,
                                      cr.Score,
                                      cr.TestDate
                                  }).ToListAsync();

            return Ok(progress);
        }

    }

}
