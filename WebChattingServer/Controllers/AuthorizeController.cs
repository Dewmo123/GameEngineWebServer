using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebChattingServer.Controllers
{
    [ApiController]
    [Route("authorize")]
    public class AuthorizeController : ControllerBase
    {
        private readonly IAuthorizeService _authorizeService;
        public AuthorizeController(IAuthorizeService service)
        {
            _authorizeService = service;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            Console.WriteLine($"Receive Login Request for {loginDTO.UserId}");
            LoginUserDTO? user = await _authorizeService.LogIn(loginDTO);
            if (user != null)
            {
                Console.WriteLine(user.user_id);
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.user_id),
                    new Claim(ClaimTypes.NameIdentifier, user.id.ToString())
                };
                user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.ToString())));

                var claimsIdentity = new ClaimsIdentity(claims, "UserKey");
                var authProperties = new AuthenticationProperties { IsPersistent = true };
                await HttpContext.SignInAsync("UserKey", new ClaimsPrincipal(claimsIdentity), authProperties);

                return Ok(new { Message = "Login successful", UserId = user.user_id });
            }
            else
            {
                Console.WriteLine("Non-Valid User");
                return Unauthorized(new { Message = "Invalid credentials" });
            }
        }

        [HttpGet("test")]
        public string Test()
        {
            string n = HttpContext.User.Identity.Name;
            Console.WriteLine(n);
            return n;
        }
    }
}
