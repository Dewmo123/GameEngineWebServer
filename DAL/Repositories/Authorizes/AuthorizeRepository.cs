using Dapper;
using DAL.VOs;
using MySql.Data.MySqlClient;

namespace DAL.Repositories.Authorizes
{
    public class AuthorizeRepository : Repository, IAuthorizeRepository
    {
        public AuthorizeRepository(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction)
        {
        }

        public async Task<LoginVO?> GetUser(string userId, string password)
        {
            string query = "SELECT * FROM LoginData WHERE UserId = @userId AND `Password` = sha2(@password,256)";
            return await _connection.QueryFirstOrDefaultAsync<LoginVO>(query, new { userId, password }, _transaction);
        }

        public async Task<LoginVO?> GetUserByUserId(string userId)
        {
            string query = "SELECT * FROM LoginData WHERE UserId = @userId";
            return await _connection.QueryFirstOrDefaultAsync<LoginVO>(query, new { userId }, _transaction);
        }

        public async Task<int> AddUser(string id, string password)
        {
            string query = "INSERT INTO LoginData (UserId, `Password`)VALUES (@id,sha2(@password,256));SELECT LAST_INSERT_ID()";
            return await _connection.ExecuteScalarAsync<int>(query, new { id, password }, _transaction);
        }
    }
}
