using System.Collections.Concurrent;

namespace BLL.Caching
{
    public class PlayerManager : IPlayerManager
    {
        private readonly ConcurrentDictionary<int, Player> _players = new();

        public bool TryGetPlayer(int id, out Player? player)
        {
            return _players.TryGetValue(id, out player);
        }

        public Player GetOrAddPlayer(int id, Func<Player> factory)
        {
            return _players.GetOrAdd(id, _ => factory());
        }

        public bool TryRemovePlayer(int id, out Player? player)
        {
            return _players.TryRemove(id, out player);
        }
    }
}
