namespace DAL.VOs
{
    public enum StatType
    {
        None = 0,
        AttackPower,
        AttackSpeed,
        Health,
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
}
