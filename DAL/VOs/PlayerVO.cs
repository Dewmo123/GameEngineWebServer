namespace DAL.VOs
{
    public enum StatType
    {
        None = 0,
        AttackPower,
        AttackSpeed,
        Health,
    }
    public enum GoodsType
    {
        None = 0,
        Gold,
    }
    internal class PlayerVO
    {
    }
    public record class StatVO
    {
        public int Id { get; set; }
        public StatType StatType { get; set; }
        public int Level { get; set; }
    }
    public record class GoodsVO
    {
        public int Id { get; set; }
        public GoodsType GoodsType { get; set; }
        public int Amount { get; set; }
    }
    public record class SkillVO
    {
        public int Id { get; set; }
        public string? SkillName { get; set; }
        public int Level { get; set; }
        public int Upgrade { get; set; }
        public int Amount { get; set; }
    }
    public record class ChapterVO
    {
        public int Chapter { get; set; }
        public int Stage { get; set; }
    }
}
