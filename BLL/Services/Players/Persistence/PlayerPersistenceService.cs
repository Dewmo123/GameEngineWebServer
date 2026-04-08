using BLL.Common.Results;
using BLL.Domain.Players;
using BLL.UoW;

namespace BLL.Services.Players.Persistence
{
    public class PlayerPersistenceService : IPlayerPersistenceService
    {
        private readonly IReadOnlyList<IPlayerPersistenceSection> _sections;
        private readonly IUnitOfWorkFactory _unitOfWorkFactory;

        public PlayerPersistenceService(IEnumerable<IPlayerPersistenceSection> sections, IUnitOfWorkFactory unitOfWorkFactory)
        {
            _sections = sections.ToArray();
            _unitOfWorkFactory = unitOfWorkFactory;
        }

        public async Task<PlayerState> LoadAsync(int playerId)
        {
            await using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateAsync();

            PlayerState player = new() { Id = playerId };
            foreach (IPlayerPersistenceSection section in _sections)
                await section.LoadAsync(playerId, player, unitOfWork);

            player.ApplyDefaults();
            await unitOfWork.CommitAsync();
            return player;
        }

        public async Task<Result> SaveAsync(PlayerState player)
        {
            await using IUnitOfWork unitOfWork = await _unitOfWorkFactory.CreateAsync();

            PlayerState normalized = player.Clone();
            normalized.ApplyDefaults();

            foreach (IPlayerPersistenceSection section in _sections)
            {
                Result saveResult = await section.SaveAsync(normalized, unitOfWork);
                if (!saveResult.Succeeded)
                {
                    await unitOfWork.RollbackAsync();
                    return saveResult;
                }
            }

            await unitOfWork.CommitAsync();
            return Result.Success();
        }
    }
}
