using DAL.VOs;

namespace DAL.Repositories.Players
{
    public interface IRoleRepository
    {
        Task<int> AddRole(int id, Role role);
        Task<List<UserRoleVO>> GetUserRoles(int id);
    }
}
