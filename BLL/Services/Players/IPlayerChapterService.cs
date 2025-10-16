using BLL.Caching;
using BLL.DTOs;

namespace BLL.Services.Players
{
    public interface IPlayerChapterService
    {
        ChapterDTO ChapterChanged(Player player, int chapter);
        ChapterDTO StageChanged(Player player, int stage);
        void EnemyDead(Player player, int count);
    }
}
