using BLL.Caching;
using BLL.Common.Results;

namespace BLL.Services.Players.Session
{
    public interface IPlayerSessionService
    {
        Task<Player> GetOrLoadPlayerAsync(int id);
        Task<Result> UnloadPlayerAsync(int id);
    }
}
