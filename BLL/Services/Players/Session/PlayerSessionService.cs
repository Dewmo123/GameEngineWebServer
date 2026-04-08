using BLL.Caching;
using BLL.Common.Results;
using BLL.Domain.Players;
using BLL.Services.Players.Persistence;

namespace BLL.Services.Players.Session
{
    public class PlayerSessionService : IPlayerSessionService
    {
        private readonly IPlayerManager _playerManager;
        private readonly IPlayerPersistenceService _playerPersistenceService;

        public PlayerSessionService(IPlayerManager playerManager, IPlayerPersistenceService playerPersistenceService)
        {
            _playerManager = playerManager;
            _playerPersistenceService = playerPersistenceService;
        }

        public async Task<Player> GetOrLoadPlayerAsync(int id)
        {
            if (_playerManager.TryGetPlayer(id, out Player? existingPlayer) && existingPlayer != null)
                return existingPlayer;

            PlayerState state = await _playerPersistenceService.LoadAsync(id);
            return _playerManager.GetOrAddPlayer(id, () => new Player(state));
        }

        public async Task<Result> UnloadPlayerAsync(int id)
        {
            if (!_playerManager.TryGetPlayer(id, out Player? player) || player == null)
                return Result.Success();

            Result saveResult = await _playerPersistenceService.SaveAsync(player.GetSnapshot());
            if (!saveResult.Succeeded)
                return saveResult;

            _playerManager.TryRemovePlayer(id, out _);
            return Result.Success();
        }
    }
}
