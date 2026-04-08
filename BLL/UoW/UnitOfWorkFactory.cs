namespace BLL.UoW
{
    public class UnitOfWorkFactory : IUnitOfWorkFactory
    {
        private readonly string _connectionString;

        public UnitOfWorkFactory(string connectionString)
        {
            _connectionString = string.IsNullOrWhiteSpace(connectionString)
                ? throw new ArgumentException("Connection string is required.", nameof(connectionString))
                : connectionString;
        }

        public Task<IUnitOfWork> CreateAsync()
        {
            return UnitOfWork.CreateAsync(_connectionString);
        }
    }
}
