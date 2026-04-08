using BLL.Common.Results;
using BLL.DTOs;
using BLL.Services.Authorizes;
using BLL.Services.Players.Application;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace WebChattingServer.Controllers
{
    [Route("authorize")]
    public class AuthorizeController : ApiResultControllerBase
    {
        private readonly IAuthorizeService _authorizeService;
        private readonly IPlayerApplicationService _playerApplicationService;

        public AuthorizeController(IAuthorizeService authorizeService, IPlayerApplicationService playerApplicationService)
        {
            _authorizeService = authorizeService;
            _playerApplicationService = playerApplicationService;
        }

        [HttpPost("log-in")]
        public async Task<IActionResult> Login([FromBody] LoginDTO loginDTO)
        {
            Result<LoginUserDTO> loginResult = await _authorizeService.LogIn(loginDTO);
            if (!loginResult.Succeeded)
                return ToActionResult(loginResult);

            LoginUserDTO user = loginResult.Value!;

            List<Claim> claims =
            [
                new Claim(ClaimTypes.Name, user.UserId),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            ];

            foreach (var role in user.Roles)
                claims.Add(new Claim(ClaimTypes.Role, role.ToString()));

            ClaimsIdentity claimsIdentity = new(claims, "UserKey");
            AuthenticationProperties authProperties = new() { IsPersistent = true };
            await HttpContext.SignInAsync("UserKey", new ClaimsPrincipal(claimsIdentity), authProperties);
            return Ok(new { Message = "Login successful", UserId = user.UserId });
        }

        [HttpDelete("log-out")]
        public async Task<IActionResult> LogOut()
        {
            string? id = HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrWhiteSpace(id) && int.TryParse(id, out int playerId))
            {
                Result unloadResult = await _playerApplicationService.LogOutAsync(playerId);
                if (!unloadResult.Succeeded)
                    return ToActionResult(unloadResult);
            }

            await HttpContext.SignOutAsync("UserKey");
            return Ok();
        }

        [HttpPost("sign-up")]
        public async Task<IActionResult> SignUp([FromBody] CreateUserDTO createUser)
        {
            Result result = await _authorizeService.SignUp(createUser);
            return result.Succeeded
                ? Created(string.Empty, new { Message = "Sign up success" })
                : ToActionResult(result);
        }
    }
}
