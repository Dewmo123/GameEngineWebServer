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
            Player player = new(id,playerInfo.Chapter!, playerInfo.Stats!, playerInfo.Goods!, playerInfo.Skills!);
            Console.WriteLine("add");
            return _players.TryAdd(id, player);
        }

        public Player GetPlayer(int id)
        {
            return _players[id];
        }

        public async Task<bool> RemovePlayer(int id)
        {
            bool success = false;
            if (_players.TryRemove(id, out var removePlayer))
            {
                await using (IUnitOfWork uow = await UnitOfWork.CreateUoWAsync())
                {
                    success = true;
                    success &= await removePlayer.UpdateStats(uow.Stat);
                    success &= await removePlayer.UpdateGoods(uow.Goods);
                    success &= await removePlayer.UpdateChapter(uow.Chapter);
                    success &= await removePlayer.UpdateSkill(uow.Skill);
                    if (!success)
                        await uow.RollbackAsync();
                }
            }
            return success;
        }
    }
}