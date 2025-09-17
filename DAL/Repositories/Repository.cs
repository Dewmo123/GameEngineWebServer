using MySql.Data.MySqlClient;

namespace DAL.Repositories
{
    public abstract class Repository
    {
        protected MySqlConnection _connection;
        protected MySqlTransaction _transaction;
        public Repository(MySqlConnection connection, MySqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
    }
}
