using BLL.Caching;
using BLL.DTOs;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.Repositories.Players.Equip;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerSkillEquipPersistenceSection : IPlayerPersistenceSection
    {
        public async Task LoadAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            ISkillEquipRepository skillEquipRepository = unitOfWork.GetRepository<ISkillEquipRepository, SkillEquipRepository>();
            List<SkillEquipVO> skillEquips = await skillEquipRepository.GetSkillEquips(playerId);

            player.SkillEquips = new string?[DefaultSetting.skillEquipLength];
            foreach (SkillEquipVO skillEquip in skillEquips)
            {
                if (skillEquip.Idx < 0 || skillEquip.Idx >= player.SkillEquips.Length)
                    continue;

                player.SkillEquips[skillEquip.Idx] = skillEquip.SkillName;
            }

            if (skillEquips.Count == 0)
            {
                foreach (int idx in DefaultSetting.defaultSkillEquip)
                    await skillEquipRepository.AddSkillEquip(playerId, idx, string.Empty);
            }
        }

        public async Task<bool> SaveAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            if (player.SkillEquips.Length != DefaultSetting.skillEquipLength)
                return false;

            ISkillEquipRepository skillEquipRepository = unitOfWork.GetRepository<ISkillEquipRepository, SkillEquipRepository>();
            for (int i = 0; i < player.SkillEquips.Length; i++)
            {
                if (await skillEquipRepository.UpdateSkillEquip(playerId, i, player.SkillEquips[i] ?? string.Empty) != 1)
                    return false;
            }

            return true;
        }
    }
}
