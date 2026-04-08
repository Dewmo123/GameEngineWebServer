using BLL.Common.Results;
using BLL.Domain.Players;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerChapterPersistenceSection : IPlayerPersistenceSection
    {
        public async Task LoadAsync(int playerId, PlayerState player, IUnitOfWork unitOfWork)
        {
            ChapterVO? chapter = await unitOfWork.Chapter.GetChapter(playerId);
            if (chapter == null)
                return;

            player.Chapter = new PlayerChapterState
            {
                Chapter = chapter.Chapter,
                Stage = chapter.Stage,
                EnemyCount = chapter.EnemyCount
            };
        }

        public async Task<Result> SaveAsync(PlayerState player, IUnitOfWork unitOfWork)
        {
            int affected = await unitOfWork.Chapter.UpdateChapter(player.Id, player.Chapter.Chapter, player.Chapter.Stage, player.Chapter.EnemyCount);
            if (affected == 0)
                affected = await unitOfWork.Chapter.AddChapter(player.Id, player.Chapter.Chapter, player.Chapter.Stage, player.Chapter.EnemyCount);

            return affected == 1
                ? Result.Success()
                : Result.PersistenceFailure("Failed to persist chapter.");
        }
    }
}
