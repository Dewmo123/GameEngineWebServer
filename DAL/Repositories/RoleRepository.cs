using DAL.VOs;
using Dapper;
using MySql.Data.MySqlClient;
using System.Data.Common;

namespace DAL.Repositories
{
    public class RoleRepository : Repository, IRoleRepository
    {
        public RoleRepository(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction)
        {
        }

        public async Task<int> AddRole(int id, Role role)
        {
            string query = "INSERT INTO RoleData (Id, Role) VALUES (@id, @role)";
            return await _connection.ExecuteAsync(query, new { id, role });
        }

        public async Task<List<UserRoleVO>> GetUserRoles(int id)
        {
            string query = "SELECT `Role` FROM RoleData WHERE Id = @id";
            var vos = await _connection.QueryAsync<UserRoleVO>(sql: query, new { id }, transaction: _transaction);
            return vos.ToList();
        }

    }
}
