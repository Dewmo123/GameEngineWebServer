using BLL.DTOs;

namespace BLL.Caching
{
    public interface IPlayerManager
    {
        bool RemovePlayer(int id,out Player? player);
        bool AddPlayer(int id, PlayerDTO playerInfo);
        Player GetPlayer(int id);
    }
}
