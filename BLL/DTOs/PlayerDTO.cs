using BLL.Caching;
using DAL.VOs;

namespace BLL.DTOs
{
    public record class PlayerDTO
    {
        public ChapterDTO? Chapter { get; set; }
        public Dictionary<StatType, int>? Stats { get; set; }
        public Dictionary<GoodsType, int>? Goods { get; set; }
        public Dictionary<string,SkillDTO>? Skills { get; set; }
        public Dictionary<string, PartnerDTO>? Partners { get; set; }
        public string?[] SkillEquips { get; set; } = new string?[DefaultSetting.skillEquipLength];
        public string?[] PartnerEquips { get; set; } = new string?[DefaultSetting.partnerEquipLength];
    }
    public record class StatDTO
    {
        public StatType StatType { get; set; }
        public int Level { get; set; }
    }
    public record class GoodsDTO
    {
        public GoodsType GoodsType { get; set; }
        public int Amount { get; set; }
    }
    public record class SkillDTO
    {
        public string? SkillName { get; set; }
        public int Level { get; set; }
        public int Upgrade { get; set; }
        public int Amount { get; set; }
    }
    public record class SkillAmountDTO
    {
        public string? SkillName { get; set; }
        public int Amount { get; set; }
    }
    public record class PartnerDTO
    {
        public string? PartnerName { get; set; }
        public int Level { get; set; }
        public int Upgrade { get; set; }
        public int Amount { get; set; }
    }
    public record class ChapterDTO
    {
        public int Chapter { get; set; }
        public int Stage { get; set; }
        public int EnemyCount { get; set; }
    }
    public record class EnemyDeadDTO
    {
        public int EnemyCount { get; set; }
    }
    public record class SkillEquipDTO
    {
        public int Idx { get; set; }
        public string? SkillName { get; set; }
    }
}
