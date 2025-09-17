using DAL.Repositories;

namespace BLL.UoW
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IAuthorizeRepository Auth { get; }
        IRoleRepository Role { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
