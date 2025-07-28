using DAL.VOs;

namespace DAL.Repositories
{
    public interface IAuthorizeReporitory
    {
        Task<List<UserRoleVO>> GetUserRoles(int id);
        Task<LoginVO?> Login(string id,string password);
    }
}
