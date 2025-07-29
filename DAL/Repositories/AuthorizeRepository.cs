using DAL.VOs;
using MySql.Data.MySqlClient;

namespace DAL.Repositories
{
    public class AuthorizeRepository : IAuthorizeRepository
    {
        private string _connectionString;
        public AuthorizeRepository(string conn)
        {
            _connectionString = conn;
        }
        public async Task<List<UserRoleVO>> GetUserRoles(int id)
        {
            using (MySqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT `role` FROM user_role_data WHERE id = @id";
                command.Parameters.AddWithValue("@id", id);
                var table = await command.ExecuteReaderAsync();
                List<UserRoleVO> roles = new();
                while (await table.ReadAsync())
                {
                    string roleStr = table.GetString(table.GetOrdinal("role"));
                    UserRoleVO role = new UserRoleVO() { Role = Enum.Parse<Role>(roleStr) };
                    roles.Add(role);
                }
                return roles;
            }
        }

        public async Task<LoginVO?> Login(string userId, string password)
        {
            using (MySqlConnection connection = new(_connectionString))
            {
                await connection.OpenAsync();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = $"SELECT * FROM user_login_data WHERE user_id = @userId AND `password` = sha2(@password,256)";
                command.Parameters.AddWithValue("@password", password);
                command.Parameters.AddWithValue("@userId", userId);
                var table = await command.ExecuteReaderAsync();
                while (await table.ReadAsync())
                {
                    LoginVO login = new();
                    login.Id = table.GetInt32(table.GetOrdinal("id"));
                    login.UserId = table.GetString(table.GetOrdinal("user_id"));
                    return login;
                }
                return null;
            }
        }
    }
}
