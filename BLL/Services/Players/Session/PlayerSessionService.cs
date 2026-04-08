using BLL.Caching;
using BLL.DTOs;
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

        public async Task<PlayerDTO> LoadPlayerAsync(int id)
        {
            PlayerDTO player = await _playerPersistenceService.LoadAsync(id);
            player.Id = id.ToString();
            _playerManager.AddPlayer(id, player);
            return player;
        }

        public async Task<bool> UnloadPlayerAsync(int id)
        {
            if (!_playerManager.RemovePlayer(id, out Player? player) || player == null)
                return false;

            return await _playerPersistenceService.SaveAsync(id, player.GetCopyDTO());
        }
    }
}
