using DAL.VOs;
using Dapper;
using MySql.Data.MySqlClient;

namespace DAL.Repositories.Players
{
    public class StatRepository : Repository, IStatRepository
    {
        public StatRepository(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction)
        {
        }


        public async Task<List<StatVO>> GetStatsAsync(int id)
        {
            string query = "SELECT * FROM PlayerStat WHERE Id = @id";
            var vos = await _connection.QueryAsync<StatVO>(sql: query, new { id });
            return vos.ToList();
        }
        public async Task<int> AddStatAsync(int id, StatType statType,int level)
        {
            string query = "INSERT INTO PlayerStat (Id,StatType,Level) VALUES (@id,@statType,@Level)";
            return await _connection.ExecuteAsync(sql: query,new { id, statType,level});
        }
    }
}
