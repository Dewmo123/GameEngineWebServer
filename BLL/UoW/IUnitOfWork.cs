using DAL.Repositories;

namespace BLL.UoW
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IAuthorizeRepository Auth { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
