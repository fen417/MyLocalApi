using Microsoft.AspNetCore.Mvc;
using MyLocalApi.Models;
using MyLocalApi.Services;
using Microsoft.AspNetCore.Authorization;
using MyLocalApi.Data;


namespace MyLocalApi.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private static List<User> _users = new()
    {
        new User
        {
            Id = Guid.NewGuid(),
            FullName = "Тестовый Пользователь",
            Email = "test@mail.com",
            Password = "123456",
            Role = "admin"
        }
    };

        [Authorize(Roles = "admin")]
        [HttpPost("register")]
        public IActionResult Register(RegisterRequest request)
        {
            if (UserStore.Users.Any(u => u.Email == request.Email))
                return BadRequest(new { error = "Email already in use" });

            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = request.FullName,
                Email = request.Email,
                Password = request.Password,
                Role = request.Role ?? "user",
                GroupName = request.GroupName
            };

            UserStore.Users.Add(user);
            return Ok(user);
        }

        [HttpPost("dev-create-user")]
        public IActionResult DevCreateUser()
        {
            var user = new User
            {
                Id = Guid.NewGuid(),
                FullName = "Тестовый Пользователь",
                Email = "test@mail.com",
                Password = "123456",
                Role = "admin"
            };

            _users.Add(user);
            return Ok(user);
        }

        [HttpPost("login")]
        public IActionResult Login(LoginRequest request)
        {
            var user = _users.FirstOrDefault(u =>
                u.Email == request.Email && u.Password == request.Password);

            if (user == null)
                return BadRequest(new { error = "Invalid email or password" });

            var token = TokenService.GenerateToken(user);

            return Ok(new
            {
                token,
                expiresAt = DateTime.UtcNow.AddHours(1),
                user = new
                {
                    user.Id,
                    user.FullName,
                    user.Role
                }
            });
        }
    }

}
