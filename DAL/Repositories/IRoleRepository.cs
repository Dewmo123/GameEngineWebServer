using DAL.VOs;

namespace DAL.Repositories
{
    public interface IRoleRepository
    {
        Task<int> AddRole(int id, Role role);
        Task<List<UserRoleVO>> GetUserRoles(int id);
    }
}
