using BLL.Common.Results;
using BLL.Domain.Players;
using BLL.UoW;

namespace BLL.Services.Players.Persistence
{
    public interface IPlayerPersistenceSection
    {
        Task LoadAsync(int playerId, PlayerState player, IUnitOfWork unitOfWork);
        Task<Result> SaveAsync(PlayerState player, IUnitOfWork unitOfWork);
    }
}
