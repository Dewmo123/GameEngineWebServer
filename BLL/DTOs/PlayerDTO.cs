using BLL.Caching;
using DAL.VOs;

namespace BLL.DTOs
{
    public record class PlayerDTO
    {
        public string? Id { get; set; }
        public ChapterDTO Chapter { get; set; } = new();
        public Dictionary<StatType, int> Stats { get; set; } = new();
        public Dictionary<GoodsType, int> Goods { get; set; } = new();
        public Dictionary<string,SkillDTO> Skills { get; set; } = new();
        public Dictionary<string, PartnerDTO> Partners { get; set; } = new();
        public string?[] SkillEquips { get; set; } = new string?[DefaultSetting.skillEquipLength];
        public string?[] PartnerEquips { get; set; } = new string?[DefaultSetting.partnerEquipLength];
    }
}
