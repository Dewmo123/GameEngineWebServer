using DAL.VOs;
using Dapper;
using MySql.Data.MySqlClient;

namespace DAL.Repositories.Players.Equip
{
    public class PartnerEquipRepository : Repository, IPartnerEquipRepository
    {
        public PartnerEquipRepository(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction)
        {
        }

        public async Task<int> AddPartnerEquip(int id, int idx, string partnerName)
        {
            string query = "INSERT INTO PlayerPartnerEquip (Id,Idx,PartnerName) VALUES (@id,@idx,@partnerName)";
            return await _connection.ExecuteAsync(sql: query, new { id, idx, partnerName });
        }

        public async Task<List<PartnerEquipVO>> GetPartnerEquips(int id)
        {
            string query = "SELECT * FROM PlayerPartnerEquip WHERE Id = @id";
            var vos = await _connection.QueryAsync<PartnerEquipVO>(sql: query, new { id });
            return vos.ToList();
        }

        public async Task<int> UpdatePartnerEquip(int id, int idx, string partnerName)
        {
            string query = "UPDATE PlayerPartnerEquip SET PartnerName = @partnerName WHERE Id = @id AND Idx = @idx";
            return await _connection.ExecuteAsync(sql: query, new { partnerName, id, idx });
        }
    }
}
