namespace BLL.DTOs
{
    public record class ChapterDTO
    {
        public int Chapter { get; set; }
        public int Stage { get; set; }
        public int EnemyCount { get; set; }
    }
    public record class EnemyDeadDTO
    {
        public int EnemyCount { get; set; }
    }
    public record class ChangeChapterDTO
    {
        public int Chapter { get; set; }
    }
    public record class ChangeStageDTO
    {
        public int Stage { get; set; }
    }
}
