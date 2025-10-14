using BLL.DTOs;
using BLL.UoW;
using System.Collections.Concurrent;

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
            Player player = new(id, playerInfo);
            Console.WriteLine("add");
            return _players.TryAdd(id, player);
        }

        public Player GetPlayer(int id)
        {
            return _players[id];
        }

        public bool RemovePlayer(int id,out Player? player)
        {
            bool success = _players.TryRemove(id, out var removePlayer);
            player = removePlayer;
            return success;
        }
    }
}