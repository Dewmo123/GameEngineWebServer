
using DAL.VOs;
using Dapper;
using MySql.Data.MySqlClient;

namespace DAL.Repositories.Players
{
    public class ChapterRepository : Repository, IChapterRepository
    {
        public ChapterRepository(MySqlConnection connection, MySqlTransaction transaction) : base(connection, transaction)
        {
        }

        public Task<int> AddChapter(int id, int chapter, int stage,int enemyCount)
        {
            string query = "INSERT INTO PlayerChapterData (Id,Chapter,Stage,EnemyCount) VALUES(@id,@chapter,@stage,@enemyCount)";
            return _connection.ExecuteAsync(sql: query, new { id, chapter, stage,enemyCount });
        }

        public async Task<ChapterVO?> GetChapter(int id)
        {
            string query = "SELECT * FROM PlayerChapterData WHERE Id = @id";
            return await _connection.QueryFirstOrDefaultAsync<ChapterVO>(sql: query, new { id });
        }

        public Task<int> UpdateChapter(int id, int chapter, int stage,int enemyCount)
        {
            string query = "UPDATE PlayerChapterData SET Chapter = @chapter, Stage = @stage, EnemyCount = @enemyCount WHERE Id = @id";
            return _connection.ExecuteAsync(sql: query, new { chapter, stage,enemyCount, id });
        }
    }
}
