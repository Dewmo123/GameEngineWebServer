using BLL.DTOs;

namespace BLL.Services
{
    public interface IPlayerService
    {
        Task SignUp(LoginUserDTO loginUserDTO);
        Task LogIn(LoginUserDTO loginUserDTO);
    }
}
