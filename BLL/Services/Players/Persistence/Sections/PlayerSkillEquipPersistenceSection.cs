using BLL.Common.Results;
using BLL.Domain.Players;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerSkillEquipPersistenceSection : IPlayerPersistenceSection
    {
        public async Task LoadAsync(int playerId, PlayerState player, IUnitOfWork unitOfWork)
        {
            List<SkillEquipVO> skillEquips = await unitOfWork.SkillEquip.GetSkillEquips(playerId);
            foreach (SkillEquipVO skillEquip in skillEquips)
            {
                if (skillEquip.Idx < 0 || skillEquip.Idx >= player.SkillEquips.Length)
                    continue;

                player.SkillEquips[skillEquip.Idx] = string.IsNullOrWhiteSpace(skillEquip.SkillName) ? null : skillEquip.SkillName;
            }
        }

        public async Task<Result> SaveAsync(PlayerState player, IUnitOfWork unitOfWork)
        {
            for (int i = 0; i < player.SkillEquips.Length; i++)
            {
                int affected = await unitOfWork.SkillEquip.UpdateSkillEquip(player.Id, i, player.SkillEquips[i] ?? string.Empty);
                if (affected == 0)
                    affected = await unitOfWork.SkillEquip.AddSkillEquip(player.Id, i, player.SkillEquips[i] ?? string.Empty);

                if (affected != 1)
                    return Result.PersistenceFailure($"Failed to persist skill equip slot '{i}'.");
            }

            return Result.Success();
        }
    }
}
