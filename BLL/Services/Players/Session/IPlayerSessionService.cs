using BLL.DTOs;

namespace BLL.Services.Players.Session
{
    public interface IPlayerSessionService
    {
        Task<PlayerDTO> LoadPlayerAsync(int id);
        Task<bool> UnloadPlayerAsync(int id);
    }
}
