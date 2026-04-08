using BLL.DTOs;

namespace BLL.Services.Players.Persistence
{
    public interface IPlayerPersistenceService
    {
        Task<PlayerDTO> LoadAsync(int playerId);
        Task<bool> SaveAsync(int playerId, PlayerDTO player);
    }
}
