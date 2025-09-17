using DAL.VOs;
using MySql.Data.MySqlClient;
using Dapper;

namespace DAL.Repositories
{
    public class AuthorizeRepository : Repository,IAuthorizeRepository
    {
        public AuthorizeRepository(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction)
        {
        }

        public async Task<LoginVO?> GetUser(string userId, string password)
        {
            string query = "SELECT * FROM LoginData WHERE UserId = @userId AND `Password` = sha2(@password,256)";
            return await _connection.QueryFirstOrDefaultAsync<LoginVO>(query, new { userId, password }, _transaction);
        }

        public async Task<int> AddUser(string id, string password)
        {
            string query = "INSERT INTO LoginData (UserId, `Password`)VALUES (@id,sha2(@password,256))";
            return await _connection.ExecuteScalarAsync<int>(query, new { id, password });
        }
    }
}
