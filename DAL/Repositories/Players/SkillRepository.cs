
using DAL.VOs;
using Dapper;
using MySql.Data.MySqlClient;

namespace DAL.Repositories.Players
{
    public class SkillRepository : Repository, ISkillRepository
    {
        public SkillRepository(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction)
        {
        }

        public async Task<int> AddSkill(int id, string skillName, int level, int upgrade, int amount)
        {
            string query = "INSERT INTO PlayerSkill (Id, SkillName, Level, Upgrade, Amount) VALUES (@id, @skillName, @level, @upgrade, @amount)";
            return await _connection.ExecuteAsync(sql: query, new { id, skillName, level, upgrade, amount });
        }

        public async Task<List<SkillVO>> GetAllSkills(int id)
        {
            string query = "SELECT * FROM PlayerSkill WHERE Id = @id";
            var vos = await _connection.QueryAsync<SkillVO>(sql: query, new { id });
            return vos.ToList();
        }

        public async Task<int> UpdateSkill(int id, string skillName, int level, int upgrade, int amount)
        {
            string query = "UPDATE PlayerSkill SET SkillName = @skillName, Level = @level, Upgrade = @upgrade, Amount = @amount WHERE Id = @id";
            return await _connection.ExecuteAsync(sql: query, new { skillName, level, upgrade, amount, id });
        }
    }
}
