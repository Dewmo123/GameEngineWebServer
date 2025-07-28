using BLL.DTOs;

namespace BLL.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        public Task<LoginUserDTO?> LogIn(LoginDTO loginDTO)
        {
            throw new Exception();
        }

        public Task SignUp(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }
    }
}
