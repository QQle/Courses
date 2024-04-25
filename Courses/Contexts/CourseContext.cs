using Courses.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Courses.Contexts
{
    public class CourseContext : IdentityDbContext<User>
    {
        public CourseContext(DbContextOptions<CourseContext> options) : base(options)
        {
            Database.EnsureCreated();
        }
        public DbSet<User> User {  get; set; }
        public DbSet<Cours> CoursesItem { get; set; }
        public DbSet<Link> Links { get; set; }
        public DbSet<UsersCourses> UserCourses { get; set; }
        public DbSet<Models.Task> Tasks { get; set; }
        public DbSet<TaskAnswer> TaskResults { get; set; }
        public DbSet<CourseResult> CourseResults { get; set; }  

        


    }
}
