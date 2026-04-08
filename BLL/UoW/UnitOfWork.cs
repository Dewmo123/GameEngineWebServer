using MySql.Data.MySqlClient;

namespace BLL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        public static string? connectionString;

        private readonly MySqlConnection _connection;
        private readonly MySqlTransaction _transaction;
        private readonly Dictionary<Type, object> _repos;
        private bool _completed;

        private UnitOfWork(MySqlConnection connection, MySqlTransaction transaction)
        {
            _repos = new();
            _connection = connection;
            _transaction = transaction;
        }

        public TInterface GetRepository<TInterface, TImpl>()
            where TInterface : class
            where TImpl : class, TInterface
        {
            Type type = typeof(TImpl);
            if (!_repos.TryGetValue(type, out object? repo))
            {
                repo = Activator.CreateInstance(type, _connection, _transaction);
                _repos[type] = repo!;
            }

            return (TInterface)repo!;
        }

        public static async Task<IUnitOfWork> CreateUoWAsync()
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new NullReferenceException();

            MySqlConnection connection = new(connectionString);
            await connection.OpenAsync();
            MySqlTransaction transaction = (MySqlTransaction)await connection.BeginTransactionAsync();
            return new UnitOfWork(connection, transaction);
        }

        public async Task CommitAsync()
        {
            if (_completed)
                return;

            try
            {
                await _transaction.CommitAsync();
                _completed = true;
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            if (_completed)
                return;

            await _transaction.RollbackAsync();
            _completed = true;
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (!_completed && _transaction.Connection != null)
                    await RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                await _connection.DisposeAsync();
            }
        }
    }
}
