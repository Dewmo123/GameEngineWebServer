using DAL.VOs;

namespace DAL.Repositories.Authorizes
{
    public interface IAuthorizeRepository
    {
        Task<LoginVO?> GetUser(string id,string password);
        Task<int> AddUser(string id, string password);
    }
}
