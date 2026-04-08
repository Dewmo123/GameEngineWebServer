using BLL.Common.Results;
using BLL.Domain.Players;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerSkillPersistenceSection : IPlayerPersistenceSection
    {
        public async Task LoadAsync(int playerId, PlayerState player, IUnitOfWork unitOfWork)
        {
            List<SkillVO> skills = await unitOfWork.Skill.GetAllSkills(playerId);
            player.Skills = skills
                .Where(item => !string.IsNullOrWhiteSpace(item.SkillName))
                .ToDictionary(
                    item => item.SkillName!,
                    item => new PlayerSkillState
                    {
                        Name = item.SkillName!,
                        Level = item.Level,
                        Upgrade = item.Upgrade,
                        Amount = item.Amount
                    },
                    StringComparer.Ordinal);
        }

        public async Task<Result> SaveAsync(PlayerState player, IUnitOfWork unitOfWork)
        {
            foreach ((string skillName, PlayerSkillState skill) in player.Skills)
            {
                int affected = await unitOfWork.Skill.UpdateSkill(player.Id, skillName, skill.Level, skill.Upgrade, skill.Amount);
                if (affected == 0)
                    affected = await unitOfWork.Skill.AddSkill(player.Id, skillName, skill.Level, skill.Upgrade, skill.Amount);

                if (affected != 1)
                    return Result.PersistenceFailure($"Failed to persist skill '{skillName}'.");
            }

            return Result.Success();
        }
    }
}
