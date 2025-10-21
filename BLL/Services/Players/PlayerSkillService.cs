using BLL.Caching;
using BLL.DTOs;

namespace BLL.Services.Players
{
    public class PlayerSkillService : IPlayerSkillService
    {
        public void LevelUpSkill(Player player, string skillName, int level)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (!player.Skills.ContainsKey(skillName))
                    return;
                SkillDTO dto = player.Skills[skillName];
                dto.Level += level;
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }

        public bool AddSkillAmount(Player player, string skillName, int amount)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (string.IsNullOrEmpty(skillName) || !DefaultSetting.skills.Contains(skillName))
                    return false;
                if (player.Skills.ContainsKey(skillName))
                    player.Skills[skillName].Amount += amount;
                else
                    player.Skills.Add(skillName, new() { SkillName = skillName, Amount = amount });
                return true;
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }

        public bool EquipSkill(Player player, int idx, string? skillName)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (idx < 0 || idx >= player.SkillEquips.Length)
                    return false;
                if (!string.IsNullOrEmpty(skillName) && !player.Skills.ContainsKey(skillName))
                    return false;
                player.SkillEquips[idx] = skillName;
                return true;
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }

        public void AddSkill(Player player, SkillDTO skill)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (player.Skills.ContainsKey(skill.SkillName) || !DefaultSetting.skills.Contains(skill.SkillName))
                    return;
                player.Skills.Add(skill.SkillName, skill);
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }
    }
}
