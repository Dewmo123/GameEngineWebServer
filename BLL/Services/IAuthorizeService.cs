using BLL.DTOs;

namespace BLL.Services
{
    public interface IAuthorizeService
    {
        Task SignUp(LoginDTO loginUserDTO);
        Task<LoginUserDTO?> LogIn(LoginDTO loginUserDTO);
    }
}
