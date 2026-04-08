using BLL.Common.Results;
using BLL.DTOs;

namespace BLL.Services.Authorizes
{
    public interface IAuthorizeService
    {
        Task<Result<LoginUserDTO>> LogIn(LoginDTO loginUserDTO);
        Task<Result> SignUp(CreateUserDTO createUserDTO);
    }
}
