using AutoMapper;
using BLL.DTOs;
using DAL.Repositories;
using DAL.VOs;

namespace BLL.Services
{
    public class AuthorizeService : IAuthorizeService
    {
        IAuthorizeRepository _repository;
        IMapper _mapper;
        public AuthorizeService(IMapper mapper,IAuthorizeRepository repository)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<LoginUserDTO?> LogIn(LoginDTO loginDTO)
        {
            LoginVO? login = await _repository.Login(loginDTO.UserId, loginDTO.Password);
            if (login == null)
            {
                Console.WriteLine("Wrong user");
                return null;
            }
            LoginUserDTO user = _mapper.Map<LoginVO, LoginUserDTO>(login);
            List<UserRoleVO> roles = await _repository.GetUserRoles(login.Id);
            user.Roles = roles.Select(item => item.Role).ToList();
            return user;
        }

        public Task SignUp(LoginDTO loginDTO)
        {
            throw new NotImplementedException();
        }
    }
}
