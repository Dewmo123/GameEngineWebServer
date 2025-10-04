using AutoMapper;
using BLL.DTOs;
using BLL.UoW;
using DAL.VOs;

namespace BLL.Services.Players
{
    public class PlayerService : IPlayerService
    {
        IMapper _mapper;
        private static readonly Dictionary<StatType, int> defaultStat = new()
        {
            {StatType.AttackPower,0 },
            {StatType.AttackSpeed,0 },
            {StatType.Health,0 },
        };
        public PlayerService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<PlayerDTO?> GetPlayerInfos(int id)
        {
            await using (IUnitOfWork uow = await UnitOfWork.CreateUoWAsync())
            {
                PlayerDTO playerDTO = new();
                await AddStats(id, uow, playerDTO);
                return playerDTO;
            }
        }

        private async Task AddStats(int id, IUnitOfWork uow, PlayerDTO playerDTO)
        {
            List<StatVO> statVOs = await uow.Stat.GetStatsAsync(id);
            playerDTO.Stats = statVOs.ToDictionary(item => item.StatType, item => item.Level);
            foreach (var item in defaultStat)
                if (!playerDTO.Stats.ContainsKey(item.Key))
                {
                    playerDTO.Stats.Add(item.Key, item.Value);
                    int affected = await uow.Stat.AddStatAsync(id, item.Key,item.Value);
                    if (affected != 1)
                    {
                        await uow.RollbackAsync();
                        return;
                    }
                }
        }
    }
}
