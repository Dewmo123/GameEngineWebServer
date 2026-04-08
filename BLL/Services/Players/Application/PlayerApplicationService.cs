using BLL.Caching;
using BLL.Common.Results;
using BLL.DTOs;
using BLL.Services.Players.Session;

namespace BLL.Services.Players.Application
{
    public class PlayerApplicationService : IPlayerApplicationService
    {
        private readonly IPlayerSessionService _playerSessionService;

        public PlayerApplicationService(IPlayerSessionService playerSessionService)
        {
            _playerSessionService = playerSessionService;
        }

        public async Task<Result<PlayerDTO>> GetPlayerAsync(int playerId)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return Result<PlayerDTO>.Success(player.GetSnapshot().ToDto());
        }

        public Task<Result> LogOutAsync(int playerId)
        {
            return _playerSessionService.UnloadPlayerAsync(playerId);
        }

        public async Task<Result> ChangeGoodsAsync(int playerId, GoodsDTO goods)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return player.ChangeGoods(goods.GoodsType, goods.Amount);
        }

        public async Task<Result> LevelUpStatAsync(int playerId, StatDTO stat)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return player.LevelUpStat(stat.StatType, stat.Level);
        }

        public async Task<Result> LevelUpSkillAsync(int playerId, LevelUpSkillDTO skill)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return player.LevelUpSkill(skill.SkillName, skill.Level);
        }

        public async Task<Result> AddSkillAmountAsync(int playerId, SkillAmountDTO skill)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return player.AddSkillAmount(skill.SkillName, skill.Amount);
        }

        public async Task<Result> EquipSkillAsync(int playerId, SkillEquipDTO skill)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return player.EquipSkill(skill.Idx, skill.SkillName);
        }

        public async Task<Result> SetSkillProgressAsync(int playerId, SetSkillAmountAndUpgradeDTO skill)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return player.SetSkillProgress(skill.SkillName, skill.Amount, skill.Upgrade);
        }

        public async Task<Result> LevelUpPartnerAsync(int playerId, LevelUpPartnerDTO partner)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return player.LevelUpPartner(partner.PartnerName, partner.Level);
        }

        public async Task<Result> AddPartnerAmountAsync(int playerId, PartnerAmountDTO partner)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return player.AddPartnerAmount(partner.PartnerName, partner.Amount);
        }

        public async Task<Result> EquipPartnerAsync(int playerId, PartnerEquipDTO partner)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return player.EquipPartner(partner.Idx, partner.PartnerName);
        }

        public async Task<Result> SetPartnerProgressAsync(int playerId, SetPartnerAmountAndUpgradeDTO partner)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return player.SetPartnerProgress(partner.PartnerName, partner.Amount, partner.Upgrade);
        }

        public async Task<Result<ChapterDTO>> ChangeChapterAsync(int playerId, ChangeChapterDTO chapter)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            Result<BLL.Domain.Players.PlayerChapterState> result = player.ChangeChapter(chapter.Chapter);
            return result.Succeeded
                ? Result<ChapterDTO>.Success(result.Value!.ToDto())
                : Result<ChapterDTO>.Failure(result.Error!.Code, result.Error.Message);
        }

        public async Task<Result<ChapterDTO>> ChangeStageAsync(int playerId, ChangeStageDTO stage)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            Result<BLL.Domain.Players.PlayerChapterState> result = player.ChangeStage(stage.Stage);
            return result.Succeeded
                ? Result<ChapterDTO>.Success(result.Value!.ToDto())
                : Result<ChapterDTO>.Failure(result.Error!.Code, result.Error.Message);
        }

        public async Task<Result> EnemyDeadAsync(int playerId, EnemyDeadDTO enemyDead)
        {
            Player player = await _playerSessionService.GetOrLoadPlayerAsync(playerId);
            return player.RegisterEnemyDeath(enemyDead.EnemyCount);
        }
    }
}
