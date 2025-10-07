using DAL.VOs;

namespace BLL.DTOs
{
    public record class PlayerDTO
    {
        public Dictionary<StatType, int>? Stats { get; set; }
        public Dictionary<GoodsType, int>? Goods { get; set; }
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
}
