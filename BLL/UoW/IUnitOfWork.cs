using DAL.Repositories.Authorizes;
using DAL.Repositories.Players;

namespace BLL.UoW
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IAuthorizeRepository Auth { get; }
        IRoleRepository Role { get; }
        IStatRepository Stat { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
