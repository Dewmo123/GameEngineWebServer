using DAL.VOs;
using Dapper;
using MySql.Data.MySqlClient;

namespace DAL.Repositories.Players
{
    public class GoodsRepository : Repository, IGoodsRepository
    {
        public GoodsRepository(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction)
        {
        }

        public async Task<int> AddGoodsAsync(int id,GoodsType goodsType, int amount)
        {
            string query = "INSERT INTO PlayerGoods (Id,GoodsType,Amount) VALUES (@id, @goodsType,@amount)";
            return await _connection.ExecuteAsync(sql: query, new { id, goodsType, amount });
        }

        public async Task<List<GoodsVO>> GetAllGoods(int id)
        {
            string query = "SELECT * FROM PlayerGoods WHERE Id = @id";
            var vos = await _connection.QueryAsync<GoodsVO>(sql: query, new { id });
            return vos.ToList();
        }

        public async Task<int> UpdateGoods(int id,GoodsType goodsType, int amount)
        {
            string query = "UPDATE PlayerGoods SET Amount = @amount WHERE Id = @id AND GoodsType = @goodsType";
            return await _connection.ExecuteAsync(sql: query, new { id, goodsType, amount });
        }
    }
}
