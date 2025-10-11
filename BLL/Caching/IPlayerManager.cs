using BLL.DTOs;

namespace BLL.Caching
{
    public interface IPlayerManager
    {
        Task<bool> RemovePlayer(int id);
        bool AddPlayer(int id, PlayerDTO playerInfo);
        Player GetPlayer(int id);
    }
}
