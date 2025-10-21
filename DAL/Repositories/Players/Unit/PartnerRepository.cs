using DAL.VOs;
using Dapper;
using MySql.Data.MySqlClient;

namespace DAL.Repositories.Players.Unit
{
    public class PartnerRepository : Repository, IPartnerRepository
    {
        public PartnerRepository(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction)
        {
        }

        public async Task<int> AddPartner(int id, string partnerName, int level, int upgrade, int amount)
        {
            string query = "INSERT INTO PlayerPartner (Id, PartnerName, Level, Upgrade, Amount) VALUES (@id, @partnerName, @level, @upgrade, @amount)";
            return await _connection.ExecuteAsync(sql: query, new { id, partnerName, level, upgrade, amount });
        }

        public async Task<List<PartnerVO>> GetAllPartners(int id)
        {
            string query = "SELECT * FROM PlayerPartner WHERE Id = @id";
            var vos= await _connection.QueryAsync<PartnerVO>(sql: query, new { id });
            return vos.ToList();
        }

        public async Task<int> UpdatePartner(int id, string partnerName, int level, int upgrade, int amount)
        {
            string query = "UPDATE PlayerPartner SET Level = @level, Upgrade = @upgrade, Amount = @amount WHERE Id = @id AND PartnerName = @partnerName";
            return await _connection.ExecuteAsync(sql: query, new { level, upgrade, amount, id, partnerName });
        }
    }
}
