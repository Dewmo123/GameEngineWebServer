using BLL.Domain.Players;

namespace BLL.DTOs
{
    public static class PlayerMappingExtensions
    {
        public static PlayerDTO ToDto(this PlayerState state)
        {
            return new PlayerDTO
            {
                Id = state.Id.ToString(),
                Chapter = state.Chapter.ToDto(),
                Stats = new Dictionary<DAL.VOs.StatType, int>(state.Stats),
                Goods = new Dictionary<DAL.VOs.GoodsType, int>(state.Goods),
                Skills = state.Skills.ToDictionary(item => item.Key, item => item.Value.ToDto(), StringComparer.Ordinal),
                Partners = state.Partners.ToDictionary(item => item.Key, item => item.Value.ToDto(), StringComparer.Ordinal),
                SkillEquips = state.SkillEquips.ToArray(),
                PartnerEquips = state.PartnerEquips.ToArray()
            };
        }

        public static ChapterDTO ToDto(this PlayerChapterState state)
        {
            return new ChapterDTO
            {
                Chapter = state.Chapter,
                Stage = state.Stage,
                EnemyCount = state.EnemyCount
            };
        }

        public static SkillDTO ToDto(this PlayerSkillState state)
        {
            return new SkillDTO
            {
                SkillName = state.Name,
                Level = state.Level,
                Upgrade = state.Upgrade,
                Amount = state.Amount
            };
        }

        public static PartnerDTO ToDto(this PlayerPartnerState state)
        {
            return new PartnerDTO
            {
                PartnerName = state.Name,
                Level = state.Level,
                Upgrade = state.Upgrade,
                Amount = state.Amount
            };
        }
    }
}
