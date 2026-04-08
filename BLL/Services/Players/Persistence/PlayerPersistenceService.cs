using BLL.DTOs;
using BLL.UoW;

namespace BLL.Services.Players.Persistence
{
    public class PlayerPersistenceService : IPlayerPersistenceService
    {
        private readonly IReadOnlyList<IPlayerPersistenceSection> _sections;

        public PlayerPersistenceService(IEnumerable<IPlayerPersistenceSection> sections)
        {
            _sections = sections.ToArray();
        }

        public async Task<PlayerDTO> LoadAsync(int playerId)
        {
            await using IUnitOfWork unitOfWork = await UnitOfWork.CreateUoWAsync();

            PlayerDTO player = new();
            foreach (IPlayerPersistenceSection section in _sections)
                await section.LoadAsync(playerId, player, unitOfWork);

            await unitOfWork.CommitAsync();
            return player;
        }

        public async Task<bool> SaveAsync(int playerId, PlayerDTO player)
        {
            await using IUnitOfWork unitOfWork = await UnitOfWork.CreateUoWAsync();

            foreach (IPlayerPersistenceSection section in _sections)
            {
                if (!await section.SaveAsync(playerId, player, unitOfWork))
                {
                    await unitOfWork.RollbackAsync();
                    return false;
                }
            }

            await unitOfWork.CommitAsync();
            return true;
        }
    }
}
