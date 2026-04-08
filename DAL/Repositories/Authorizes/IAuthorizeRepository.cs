using DAL.VOs;

namespace DAL.Repositories.Authorizes
{
    public interface IAuthorizeRepository
    {
        Task<LoginVO?> GetUser(string id, string password);
        Task<LoginVO?> GetUserByUserId(string userId);
        Task<int> AddUser(string id, string password);
    }
}
