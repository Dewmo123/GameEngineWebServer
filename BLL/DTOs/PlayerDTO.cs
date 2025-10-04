using DAL.VOs;

namespace BLL.DTOs
{
    public record class PlayerDTO
    {
        public Dictionary<StatType, int>? Stats { get; set; }
    }
    public record class StatDTO
    {
        public StatType StatType{ get; set; }
        public int Level { get; set; }
    }
}
