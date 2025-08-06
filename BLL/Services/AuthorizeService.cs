using AutoMapper;
using BLL.DTOs;
using BLL.UoW;
using DAL.VOs;

namespace BLL.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        IMapper _mapper;
        public AuthorizeService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<LoginUserDTO?> LogIn(LoginDTO loginDTO)
        {
            await using (IUnitOfWork uow = await UnitOfWork.CreateUoWAsync())
            {
                LoginVO? login = await uow.Auth.Login(loginDTO.UserId, loginDTO.Password);
                if (login == null)
                {
                    Console.WriteLine("Wrong user");
                    return null;
                }
                Console.WriteLine(loginDTO.UserId);
                LoginUserDTO user = _mapper.Map<LoginVO, LoginUserDTO>(login);
                List<UserRoleVO> roles = await uow.Auth.GetUserRoles(login.id);
                user.Roles = roles.Select(item => item.Role).ToList();
                return user;
            }
        }

        public Task SignUp(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }
    }
}
