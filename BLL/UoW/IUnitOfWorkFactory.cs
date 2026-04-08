namespace BLL.UoW
{
    public interface IUnitOfWorkFactory
    {
        Task<IUnitOfWork> CreateAsync();
    }
}
