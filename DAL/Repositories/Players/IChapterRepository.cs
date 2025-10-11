using DAL.VOs;

namespace DAL.Repositories.Players
{
    public interface IChapterRepository
    {
        Task<int> AddChapter(int id, int chapter, int stage);
        Task<int> UpdateChapter(int id, int chapter, int stage);
        Task<ChapterVO?> GetChapter(int id);
    }
}
