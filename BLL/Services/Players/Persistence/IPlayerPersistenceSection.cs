using BLL.DTOs;
using BLL.UoW;

namespace BLL.Services.Players.Persistence
{
    public interface IPlayerPersistenceSection
    {
        Task LoadAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork);
        Task<bool> SaveAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork);
    }
}
