using BLL.DTOs;
using BLL.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Security.Policy;

namespace WebChattingServer.Controllers
{
    public class AuthorizeController : ControllerBase
    {
        private IAuthorizeService _authorizeService;
        public AuthorizeController(IAuthorizeService service)
        {
            _authorizeService = service;
        }
        [HttpPost]
        public async Task Login(LoginDTO loginDTO)
        {
            LoginUserDTO? user = await _authorizeService.LogIn(loginDTO);
            if (user != null)
            {
                List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name,user.UserId),
                    new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
                };
                user.Roles.ForEach(role => claims.Add(new Claim(ClaimTypes.Role, role.ToString())));

                ClaimsIdentity identity = new ClaimsIdentity(claims, "UserKey");
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(identity);
                AuthenticationProperties authProp = new AuthenticationProperties
                {
                    IsPersistent = true
                };
                await HttpContext.SignInAsync("UserKey", claimsPrincipal, authProp);
            }
            else
            {
                Console.WriteLine("Non-Valid USer");
            }
            
        }
    }
}
