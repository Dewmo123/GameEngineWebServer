using DAL.VOs;

namespace DAL.Repositories.Players
{
    public interface IChapterRepository
    {
        Task<int> AddChapter(int id, int chapter, int stage,int enemyCount);
        Task<int> UpdateChapter(int id, int chapter, int stage,int enemyCount);
        Task<ChapterVO?> GetChapter(int id);
    }
}
