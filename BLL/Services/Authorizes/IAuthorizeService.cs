using BLL.DTOs;

namespace BLL.Services.Authorizes
{
    public interface IAuthorizeService
    {
        Task<LoginUserDTO?> LogIn(LoginDTO loginUserDTO);
        Task<bool> SignUp(CreateUserDTO createUserDTO);
    }
}
