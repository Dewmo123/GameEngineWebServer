namespace BLL.DTOs
{
    public record class PartnerDTO
    {
        public string? PartnerName { get; set; }
        public int Level { get; set; }
        public int Upgrade { get; set; }
        public int Amount { get; set; }
    }

    public record class PartnerAmountDTO
    {
        public string? PartnerName { get; set; }
        public int Amount { get; set; }
    }
    public record class LevelUpPartnerDTO
    {
        public string? PartnerName { get; set; }
        public int Level { get; set; }
    }

    public record class PartnerEquipDTO
    {
        public int Idx { get; set; }
        public string? PartnerName { get; set; }
    }
}
