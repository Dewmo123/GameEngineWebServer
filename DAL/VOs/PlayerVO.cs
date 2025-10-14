namespace DAL.VOs
{
    public enum StatType
    {
        None = 0,
        AttackPower,
        AttackSpeed,
        Health,
        CriticalChance,
        CriticalDamage,
        Hps
    }
    public enum GoodsType
    {
        None = 0,
        Gold,
        Crystal,
        ReinforceStone,
        DungeonKey
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
    public record class PartnerVO
    {
        public int Id { get; set; }
        public string? PartnerName { get; set; }
        public int Level { get; set; }
        public int Upgrade { get; set; }
        public int Amount { get; set; }
    }
    public record class ChapterVO
    {
        public int Chapter { get; set; }
        public int Stage { get; set; }
        public int EnemyCount { get; set; }
    }
    public record class SkillEquipVO
    {
        public int Id { get; private set; }
        public int Idx { get; private set; }
        public string? SkillName { get; private set; }
    }
}
