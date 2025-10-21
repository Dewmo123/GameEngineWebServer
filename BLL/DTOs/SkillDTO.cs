using BLL.Caching;
using DAL.VOs;

namespace BLL.DTOs
{
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
    public record class SkillEquipDTO
    {
        public int Idx { get; set; }
        public string? SkillName { get; set; }
    }
    public record class LevelUpSkillDTO
    {
        public string? SkillName { get; set; }
        public int Level { get; set; }

    }
}
