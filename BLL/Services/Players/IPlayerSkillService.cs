using BLL.Caching;
using BLL.DTOs;

namespace BLL.Services.Players
{
    public interface IPlayerSkillService
    {
        bool LevelUpSkill(Player player, string skillName, int level);
        bool AddSkillAmount(Player player, string skillName, int amount);
        bool EquipSkill(Player player, int idx, string? skillName);
        void AddSkill(Player player, SkillDTO skill);
    }
}
