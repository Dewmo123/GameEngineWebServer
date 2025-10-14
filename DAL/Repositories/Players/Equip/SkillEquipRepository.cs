using DAL.VOs;
using Dapper;
using MySql.Data.MySqlClient;

namespace DAL.Repositories.Players.Equip
{
    public class SkillEquipRepository : Repository, ISkillEquipRepository
    {
        public SkillEquipRepository(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction)
        {
        }

        public async Task<int> AddSkillEquip(int id, int idx, string skillName)
        {
            string query = "INSERT INTO PlayerSkillEquip (Id,Idx,SkillName) VALUES (@id,@idx,@skillName)";
            return await _connection.ExecuteAsync(sql: query, new { id, idx, skillName });
        }

        public async Task<List<SkillEquipVO>> GetSkillEquips(int id)
        {
            string query = "SELECT * FROM PlayerSkillEquip WHERE Id = @id";
            var vos = await _connection.QueryAsync<SkillEquipVO>(sql: query, new { id });
            return vos.ToList();
        }

        public async Task<int> UpdateSkillEquip(int id, int idx, string skillName)
        {
            string query = "UPDATE PlayerSkillEquip SET SkillName = @skillName WHERE Id = @id AND Idx = @idx";
            return await _connection.ExecuteAsync(sql: query, new { skillName, id, idx });
        }
    }
}
