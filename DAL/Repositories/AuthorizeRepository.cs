using DAL.VOs;
using MySql.Data.MySqlClient;
using Dapper;

namespace DAL.Repositories
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private MySqlConnection _connection;
        private MySqlTransaction _transaction;
        public AuthorizeRepository(MySqlConnection connection, MySqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }
        public async Task<List<UserRoleVO>> GetUserRoles(int id)
        {
            string query = "SELECT `role` FROM user_role_data WHERE id = @id";
            var vos = await _connection.QueryAsync<UserRoleVO>(sql: query,new { id}, transaction: _transaction);
            return vos.ToList();
        }

        public async Task<LoginVO?> Login(string userId, string password)
        {
            string query = "SELECT * FROM user_login_data WHERE user_id = @userId AND `password` = sha2(@password,256)";
            return await _connection.QueryFirstOrDefaultAsync<LoginVO>(query, new { userId, password }, _transaction);
        }
    }
}
