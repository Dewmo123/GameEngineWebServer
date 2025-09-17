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
                LoginVO? login = await uow.Auth.GetUser(loginDTO.UserId, loginDTO.Password);
                if (login == null)
                {
                    Console.WriteLine("Wrong user");
                    return null;
                }
                Console.WriteLine(loginDTO.UserId);
                LoginUserDTO user = _mapper.Map<LoginVO, LoginUserDTO>(login);
                List<UserRoleVO> roles = await uow.Role.GetUserRoles(login.Id);
                user.Roles = roles.Select(item => item.Role).ToList();
                return user;
            }
        }

        public async Task<bool> SignUp(CreateUserDTO createUserDTO)
        {
            await using (IUnitOfWork uow = await UnitOfWork.CreateUoWAsync())
            {
                LoginVO? loginVO = await uow.Auth.GetUser(createUserDTO.UserId, createUserDTO.Password);
                if (loginVO != null)
                    return false;
                int id = await uow.Auth.AddUser(createUserDTO.UserId, createUserDTO.Password);
                Console.WriteLine(id);//0 반환함 고쳐야됨요
                int affected = await uow.Role.AddRole(id, Role.User);
                if (affected == 0)
                {
                    await uow.RollbackAsync();
                    return false;
                }
                return true;
            }
        }
    }
}
