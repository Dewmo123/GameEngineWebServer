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
            {StatType.AttackPower,1 },
            {StatType.AttackSpeed,1 },
            {StatType.Health,1 },
            {StatType.CriticalChance,1 },
            {StatType.CriticalDamage,1 },
        }; private static readonly Dictionary<GoodsType, int> defaultGoods = new()
        {
            {GoodsType.Gold, 0 },
            {GoodsType.Crystal, 0 },
            {GoodsType.ReinforceStone, 0 },
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
                await GetOrAddStats(id, uow, playerDTO);
                await GetOrAddGoods(id, uow, playerDTO);
                await GetSkills(id, uow, playerDTO);
                await GetOrAddChapter(id, uow, playerDTO);
                return playerDTO;
            }
        }

        private async Task GetOrAddStats(int id, IUnitOfWork uow, PlayerDTO playerDTO)
        {
            List<StatVO> statVOs = await uow.Stat.GetStats(id);
            playerDTO.Stats = statVOs.ToDictionary(item => item.StatType, item => item.Level);
            foreach (var item in defaultStat)
                if (!playerDTO.Stats.ContainsKey(item.Key))
                {
                    playerDTO.Stats.Add(item.Key, item.Value);
                    int affected = await uow.Stat.AddStat(id, item.Key,item.Value);
                    if (affected != 1)
                    {
                        await uow.RollbackAsync();
                        return;
                    }
                }
        }
        private async Task GetOrAddGoods(int id, IUnitOfWork uow, PlayerDTO playerDTO)
        {
            List<GoodsVO> goods = await uow.Goods.GetAllGoods(id);
            playerDTO.Goods = goods.ToDictionary(item => item.GoodsType, item => item.Amount);
            foreach (var item in defaultGoods)
                if (!playerDTO.Goods.ContainsKey(item.Key))
                {
                    playerDTO.Goods.Add(item.Key, item.Value);
                    int affected = await uow.Goods.AddGoodsAsync(id, item.Key, item.Value);
                    if (affected != 1)
                    {
                        Console.WriteLine("Rollback");
                        await uow.RollbackAsync();
                        return;
                    }
                }
        }
        private async Task GetSkills(int id,IUnitOfWork uow,PlayerDTO playerDTO)
        {
            List<SkillVO> vos = await uow.Skill.GetAllSkills(id);
            List<SkillDTO> dtos = _mapper.Map<List<SkillVO>, List<SkillDTO>>(vos);
            playerDTO.Skills = dtos.ToDictionary(item => item.SkillName!, item => item);
        }
        private async Task GetOrAddChapter(int id, IUnitOfWork uow, PlayerDTO playerDTO)
        {
            ChapterVO? chapter = await uow.Chapter.GetChapter(id);
            if (chapter == null) 
            {
                await uow.Chapter.AddChapter(id, 1, 1, 0);
                chapter = new ChapterVO()
                {
                    Chapter = 1,
                    Stage = 1,
                    EnemyCount = 0
                };
            }
            playerDTO.Chapter = _mapper.Map<ChapterVO,ChapterDTO>(chapter);
        }
    }
}
