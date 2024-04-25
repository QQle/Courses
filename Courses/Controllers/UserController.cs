using Courses.Contexts;
using Courses.Models;
using Courses.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Courses.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IAuthentication _authentication;
        private readonly CourseContext _courseContext;

        public UserController(IAuthentication authentication, CourseContext courseContext)
        {
            _authentication = authentication;
            _courseContext = courseContext; 
        }
        [HttpPost("Registration")]
        public async Task<IActionResult> RegisterUser([FromBody] Login user)
        {
            if (await _authentication.Registration(user))
            {
                return Ok("Вы успешно зарегистрировались");
            }
            return BadRequest("Что-то пошло не так проверьте введенные данные");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] Login user)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            var loginResult = await _authentication.Login(user);
            var currentUser = await _courseContext.Users
                .Where(x => x.UserName == user.UserName)
                .Select(x => x.Id)
                .FirstOrDefaultAsync();

            var resultObject = new
            {
                Jwt = loginResult.AccessToken,
                Refresh = loginResult.RefreshToken,
                CurrentUser = currentUser
            };
            if (loginResult.IsLogged)
            {
                return Ok(resultObject);
            }
            return Unauthorized();
        }

        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshTokenModel model)
        {
            var loginResult = await _authentication.RefreshToken(model);
            if (loginResult.IsLogged)
            {
                return Ok(loginResult);
            }
            return Unauthorized();
        }
    }
}
