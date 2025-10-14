using DAL.VOs;
using Dapper;
using MySql.Data.MySqlClient;

namespace DAL.Repositories.Players.Stat
{
    public class StatRepository : Repository, IStatRepository
    {
        public StatRepository(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction)
        {
        }


        public async Task<List<StatVO>> GetStats(int id)
        {
            string query = "SELECT * FROM PlayerStat WHERE Id = @id";
            var vos = await _connection.QueryAsync<StatVO>(sql: query, new { id });
            return vos.ToList();
        }
        public async Task<int> AddStat(int id, StatType statType,int level)
        {
            string query = "INSERT INTO PlayerStat (Id,StatType,Level) VALUES (@id,@statType,@Level)";
            return await _connection.ExecuteAsync(sql: query,new { id, statType,level});
        }

        public async Task<int> UpdateStat(int id, StatType statType, int level)
        {
            string query = "UPDATE PlayerStat SET Level = @level WHERE StatType = @statType AND Id = @id";
            return await _connection.ExecuteAsync(sql: query, new { level, statType, id });
        }
    }
}
