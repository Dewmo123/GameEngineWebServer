using BLL.Common.Results;
using BLL.DTOs;
using BLL.UoW;
using DAL.VOs;

namespace BLL.Services.Authorizes
{
    public class AuthorizeService : IAuthorizeService
    {
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public AuthorizeService(IUnitOfWorkFactory unitOfWorkFactory)
        {
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<Result<LoginUserDTO>> LogIn(LoginDTO loginDTO)
        {
            if (string.IsNullOrWhiteSpace(loginDTO.UserId) || string.IsNullOrWhiteSpace(loginDTO.Password))
                return Result<LoginUserDTO>.Invalid("UserId and Password are required.");

            await using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateAsync();

            LoginVO? login = await unitOfWork.Authorize.GetUser(loginDTO.UserId, loginDTO.Password);
            if (login == null || string.IsNullOrWhiteSpace(login.UserId))
                return Result<LoginUserDTO>.Unauthorized("Invalid credentials.");

            List<UserRoleVO> roles = await unitOfWork.Role.GetUserRoles(login.Id);

            return Result<LoginUserDTO>.Success(new LoginUserDTO
            {
                Id = login.Id,
                UserId = login.UserId,
                Roles = roles.Select(item => item.Role).ToList()
            });
        }

        public async Task<Result> SignUp(CreateUserDTO createUserDTO)
        {
            if (string.IsNullOrWhiteSpace(createUserDTO.UserId) || string.IsNullOrWhiteSpace(createUserDTO.Password))
                return Result.Invalid("UserId and Password are required.");

            await using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateAsync();

            LoginVO? existingUser = await unitOfWork.Authorize.GetUserByUserId(createUserDTO.UserId);
            if (existingUser != null)
                return Result.Conflict("User already exists.");

            int id = await unitOfWork.Authorize.AddUser(createUserDTO.UserId, createUserDTO.Password);
            if (id <= 0)
            {
                await unitOfWork.RollbackAsync();
                return Result.PersistenceFailure("Failed to create user.");
            }

            if (await unitOfWork.Role.AddRole(id, Role.User) != 1)
            {
                await unitOfWork.RollbackAsync();
                return Result.PersistenceFailure("Failed to assign default role.");
            }

            await unitOfWork.CommitAsync();
            return Result.Created("Sign up success");
        }
    }
}
