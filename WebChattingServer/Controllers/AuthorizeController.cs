using BLL.DTOs;
using BLL.Services.Authorizes;
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

        [HttpPost("log-in")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            Console.WriteLine($"Receive Login Request for {loginDTO.UserId}");
            LoginUserDTO? user = await _authorizeService.LogIn(loginDTO);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserId),
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
                };
                user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.ToString())));

                var claimsIdentity = new ClaimsIdentity(claims, "UserKey");
                var authProperties = new AuthenticationProperties { IsPersistent = true };
                await HttpContext.SignInAsync("UserKey", new ClaimsPrincipal(claimsIdentity), authProperties);
                return Ok(new { Message = "Login successful", UserId = user.UserId });
            }
            else
            {
                Console.WriteLine("Non-Valid User");
                return BadRequest(new { Message = "Login Failed" });
            }
        }
        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp(CreateUserDTO createUser)
        {
            Console.WriteLine($"SignUp Request: {createUser.UserId}");
            bool success = await _authorizeService.SignUp(createUser);
            return success ? Created("", new { Message = "SignUp success" })
                : BadRequest(new { Message = "Duplicate User" });
        }
    }
}
