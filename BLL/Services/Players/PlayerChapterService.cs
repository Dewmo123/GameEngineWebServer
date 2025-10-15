using BLL.Caching;

namespace BLL.Services.Players
{
    public class PlayerChapterService : IPlayerChapterService
    {
        public bool ChapterChanged(Player player, int chapter, int stage)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (chapter <= 0 || stage <= 0)
                    return false;
                player.Chapter.Chapter = chapter;
                player.Chapter.Stage = stage;
                player.Chapter.EnemyCount = 0;
                return true;
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }

        public void EnemyDead(Player player, int count)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                player.Chapter.EnemyCount += count;
                Console.WriteLine(player.Chapter.EnemyCount);
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }
    }
}
