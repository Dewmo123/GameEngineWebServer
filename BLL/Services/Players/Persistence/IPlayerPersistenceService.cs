using BLL.Common.Results;
using BLL.Domain.Players;

namespace BLL.Services.Players.Persistence
{
    public interface IPlayerPersistenceService
    {
        Task<PlayerState> LoadAsync(int playerId);
        Task<Result> SaveAsync(PlayerState player);
    }
}
