using AutoMapper;
using BLL.DTOs;

namespace BLL.Services.Players
{
    public interface IPlayerService
    {
        Task<PlayerDTO?> GetPlayerInfos(int id);
    }
}
