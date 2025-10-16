using BLL.Caching;
using BLL.DTOs;
using DAL.Utiles;

namespace BLL.Services.Players
{
    public class PlayerChapterService : IPlayerChapterService
    {
        public ChapterDTO ChapterChanged(Player player, int chapter)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                ChapterDTO dto = player.Chapter;
                if (chapter > 0)
                {
                    dto.Chapter += chapter;
                    dto.Stage = 1;
                    dto.EnemyCount = 0;
                }
                return Extensions.DeepClone(dto);
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

        public ChapterDTO StageChanged(Player player, int stage)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                ChapterDTO dto = player.Chapter;
                if (stage > 0)
                {
                    dto.Stage += stage;
                    dto.EnemyCount = 0;
                }
                return Extensions.DeepClone(dto);
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }
    }
}
