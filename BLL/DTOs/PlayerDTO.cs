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
}