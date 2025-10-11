
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

        public Task<int> AddChapter(int id, int chapter, int stage)
        {
            string query = "INSERT INTO PlayerChapterData (Id,Chapter,Stage) VALUES(@id,@chapter,@stage)";
            return _connection.ExecuteAsync(sql: query, new { id, chapter, stage });
        }

        public async Task<ChapterVO?> GetChapter(int id)
        {
            string query = "SELECT * FROM PlayerChapterData WHERE Id = @id";
            return await _connection.QueryFirstOrDefaultAsync<ChapterVO>(sql: query, new { id });
        }

        public Task<int> UpdateChapter(int id, int chapter, int stage)
        {
            string query = "UPDATE PlayerChapterData SET Chapter = @chapter, Stage = @stage WHERE Id = @id";
            return _connection.ExecuteAsync(sql: query, new { chapter, stage, id });
        }
    }
}
