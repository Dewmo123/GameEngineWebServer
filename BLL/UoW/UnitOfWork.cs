using DAL.Repositories;
using MySql.Data.MySqlClient;

namespace BLL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        static string connectionString = "Server=127.0.0.1;Port=3306;Database=summer_db;Uid=root;Pwd=1652;Pooling=true";
        private IAuthorizeRepository? _auth;
        private IRoleRepository? _role;
        public IAuthorizeRepository Auth => _auth ??= new AuthorizeRepository(_connection, _transaction);
        public IRoleRepository Role => _role ??= new RoleRepository(_connection, _transaction);

        private MySqlConnection _connection;
        private MySqlTransaction _transaction;
        private Dictionary<Type, Repository> _repos;
        private UnitOfWork(MySqlConnection connection, MySqlTransaction transaction)
        {
            _repos = new();
            _connection = connection;
            _transaction = transaction;
        }
        public static async Task<IUnitOfWork> CreateUoWAsync()
        {
            var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            var transaction = await connection.BeginTransactionAsync();
            return new UnitOfWork(connection, transaction);
        }
        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_transaction.Connection != null)
                    await CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                await _connection.DisposeAsync();
            }
        }
    }
}
