using BLL.Common.Results;
using BLL.DTOs;

namespace BLL.Services.Players.Application
{
    public interface IPlayerApplicationService
    {
        Task<Result<PlayerDTO>> GetPlayerAsync(int playerId);
        Task<Result> LogOutAsync(int playerId);
        Task<Result> ChangeGoodsAsync(int playerId, GoodsDTO goods);
        Task<Result> LevelUpStatAsync(int playerId, StatDTO stat);
        Task<Result> LevelUpSkillAsync(int playerId, LevelUpSkillDTO skill);
        Task<Result> AddSkillAmountAsync(int playerId, SkillAmountDTO skill);
        Task<Result> EquipSkillAsync(int playerId, SkillEquipDTO skill);
        Task<Result> SetSkillProgressAsync(int playerId, SetSkillAmountAndUpgradeDTO skill);
        Task<Result> LevelUpPartnerAsync(int playerId, LevelUpPartnerDTO partner);
        Task<Result> AddPartnerAmountAsync(int playerId, PartnerAmountDTO partner);
        Task<Result> EquipPartnerAsync(int playerId, PartnerEquipDTO partner);
        Task<Result> SetPartnerProgressAsync(int playerId, SetPartnerAmountAndUpgradeDTO partner);
        Task<Result<ChapterDTO>> ChangeChapterAsync(int playerId, ChangeChapterDTO chapter);
        Task<Result<ChapterDTO>> ChangeStageAsync(int playerId, ChangeStageDTO stage);
        Task<Result> EnemyDeadAsync(int playerId, EnemyDeadDTO enemyDead);
    }
}
