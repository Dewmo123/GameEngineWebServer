using AutoMapper;
using BLL.DTOs;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.Repositories.Players.Chapter;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerChapterPersistenceSection : IPlayerPersistenceSection
    {
        private readonly IMapper _mapper;

        public PlayerChapterPersistenceSection(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task LoadAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            IChapterRepository chapterRepository = unitOfWork.GetRepository<IChapterRepository, ChapterRepository>();
            ChapterVO? chapter = await chapterRepository.GetChapter(playerId);

            if (chapter == null)
            {
                chapter = new ChapterVO
                {
                    Chapter = 1,
                    Stage = 1,
                    EnemyCount = 0
                };

                await chapterRepository.AddChapter(playerId, chapter.Chapter, chapter.Stage, chapter.EnemyCount);
            }

            player.Chapter = _mapper.Map<ChapterVO, ChapterDTO>(chapter);
        }

        public async Task<bool> SaveAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            IChapterRepository chapterRepository = unitOfWork.GetRepository<IChapterRepository, ChapterRepository>();
            return await chapterRepository.UpdateChapter(playerId, player.Chapter.Chapter, player.Chapter.Stage, player.Chapter.EnemyCount) == 1;
        }
    }
}
