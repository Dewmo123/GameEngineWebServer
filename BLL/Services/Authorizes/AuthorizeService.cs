using AutoMapper;
using BLL.DTOs;
using BLL.UoW;
using DAL.Repositories.Authorizes;
using DAL.VOs;

namespace BLL.Services.Authorizes
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IMapper _mapper;

        public AuthorizeService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<LoginUserDTO?> LogIn(LoginDTO loginDTO)
        {
            if (string.IsNullOrWhiteSpace(loginDTO.UserId) || string.IsNullOrWhiteSpace(loginDTO.Password))
                return null;

            await using (IUnitOfWork uow = await UnitOfWork.CreateUoWAsync())
            {
                IAuthorizeRepository authorizeRepository = uow.GetRepository<IAuthorizeRepository, AuthorizeRepository>();
                IRoleRepository roleRepository = uow.GetRepository<IRoleRepository, RoleRepository>();

                LoginVO? login = await authorizeRepository.GetUser(loginDTO.UserId, loginDTO.Password);
                if (login == null)
                {
                    Console.WriteLine("Wrong user");
                    return null;
                }

                Console.WriteLine(loginDTO.UserId);
                LoginUserDTO user = _mapper.Map<LoginVO, LoginUserDTO>(login);
                List<UserRoleVO> roles = await roleRepository.GetUserRoles(login.Id);
                user.Roles = roles.Select(item => item.Role).ToList();
                return user;
            }
        }

        public async Task<bool> SignUp(CreateUserDTO createUserDTO)
        {
            if (string.IsNullOrWhiteSpace(createUserDTO.UserId) || string.IsNullOrWhiteSpace(createUserDTO.Password))
                return false;

            await using (IUnitOfWork uow = await UnitOfWork.CreateUoWAsync())
            {
                IAuthorizeRepository authorizeRepository = uow.GetRepository<IAuthorizeRepository, AuthorizeRepository>();
                IRoleRepository roleRepository = uow.GetRepository<IRoleRepository, RoleRepository>();

                LoginVO? loginVO = await authorizeRepository.GetUser(createUserDTO.UserId, createUserDTO.Password);
                if (loginVO != null)
                    return false;

                int id = await authorizeRepository.AddUser(createUserDTO.UserId, createUserDTO.Password);
                if (await roleRepository.AddRole(id, Role.User) != 1)
                {
                    await uow.RollbackAsync();
                    return false;
                }

                await uow.CommitAsync();
                return true;
            }
        }
    }
}
