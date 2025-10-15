using BLL.Caching;

namespace BLL.Services.Players
{
    public interface IPlayerChapterService
    {
        bool ChapterChanged(Player player, int chapter, int stage);
        void EnemyDead(Player player, int count);
    }
}
