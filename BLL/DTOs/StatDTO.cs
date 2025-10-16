using DAL.VOs;

namespace BLL.DTOs
{
    public record class StatDTO
    {
        public StatType StatType { get; set; }
        public int Level { get; set; }
    }
}
