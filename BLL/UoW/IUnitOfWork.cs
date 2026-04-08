namespace BLL.UoW
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        TInterface GetRepository<TInterface, TImpl>()
            where TInterface : class
            where TImpl : class, TInterface;

        Task CommitAsync();
        Task RollbackAsync();
    }
}
