using AutoMapper;
using BLL.Caching;
using BLL.DTOs;

namespace BLL.Services.Players
{
    public interface IPlayerService
    {
        Task<PlayerDTO?> GetPlayerInfos(int id);
        Task<bool> UpdatePlayer(int id,PlayerDTO player);
    }
}
