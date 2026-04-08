using BLL.Caching;
using BLL.DTOs;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.Repositories.Players.Equip;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerPartnerEquipPersistenceSection : IPlayerPersistenceSection
    {
        public async Task LoadAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            IPartnerEquipRepository partnerEquipRepository = unitOfWork.GetRepository<IPartnerEquipRepository, PartnerEquipRepository>();
            List<PartnerEquipVO> partnerEquips = await partnerEquipRepository.GetPartnerEquips(playerId);

            player.PartnerEquips = new string?[DefaultSetting.partnerEquipLength];
            foreach (PartnerEquipVO partnerEquip in partnerEquips)
            {
                if (partnerEquip.Idx < 0 || partnerEquip.Idx >= player.PartnerEquips.Length)
                    continue;

                player.PartnerEquips[partnerEquip.Idx] = partnerEquip.PartnerName;
            }

            if (partnerEquips.Count == 0)
            {
                foreach (int idx in DefaultSetting.defaultPartnerEquip)
                    await partnerEquipRepository.AddPartnerEquip(playerId, idx, string.Empty);
            }
        }

        public async Task<bool> SaveAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            if (player.PartnerEquips.Length != DefaultSetting.partnerEquipLength)
                return false;

            IPartnerEquipRepository partnerEquipRepository = unitOfWork.GetRepository<IPartnerEquipRepository, PartnerEquipRepository>();
            for (int i = 0; i < player.PartnerEquips.Length; i++)
            {
                if (await partnerEquipRepository.UpdatePartnerEquip(playerId, i, player.PartnerEquips[i] ?? string.Empty) != 1)
                    return false;
            }

            return true;
        }
    }
}
