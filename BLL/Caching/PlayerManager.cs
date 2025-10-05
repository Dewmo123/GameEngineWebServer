using BLL.DTOs;
using BLL.UoW;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace BLL.Caching
{
    public class PlayerManager : IPlayerManager
    {
        private ConcurrentDictionary<int, Player> _players;
        public PlayerManager()
        {
            _players = new();
        }
        public bool AddPlayer(int id, PlayerDTO playerInfo)
        {
            Player player = new(id,playerInfo.Stats);
            return _players.TryAdd(id, player);
        }

        public Player GetPlayer(int id)
        {
            return _players[id];
        }

        public async Task RemovePlayer(int id)
        {
            if (_players.TryGetValue(id, out var removePlayer))
            {
                await using (IUnitOfWork uow = await UnitOfWork.CreateUoWAsync())
                {
                    bool success = true;
                    success &= await removePlayer.UpdateStats(uow.Stat);
                    if (!success)
                        await uow.RollbackAsync();
                }
            }
        }
    }
}