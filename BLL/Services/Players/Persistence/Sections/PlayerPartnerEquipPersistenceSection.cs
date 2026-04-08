using BLL.Common.Results;
using BLL.Domain.Players;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerPartnerEquipPersistenceSection : IPlayerPersistenceSection
    {
        public async Task LoadAsync(int playerId, PlayerState player, IUnitOfWork unitOfWork)
        {
            List<PartnerEquipVO> partnerEquips = await unitOfWork.PartnerEquip.GetPartnerEquips(playerId);
            foreach (PartnerEquipVO partnerEquip in partnerEquips)
            {
                if (partnerEquip.Idx < 0 || partnerEquip.Idx >= player.PartnerEquips.Length)
                    continue;

                player.PartnerEquips[partnerEquip.Idx] = string.IsNullOrWhiteSpace(partnerEquip.PartnerName) ? null : partnerEquip.PartnerName;
            }
        }

        public async Task<Result> SaveAsync(PlayerState player, IUnitOfWork unitOfWork)
        {
            for (int i = 0; i < player.PartnerEquips.Length; i++)
            {
                int affected = await unitOfWork.PartnerEquip.UpdatePartnerEquip(player.Id, i, player.PartnerEquips[i] ?? string.Empty);
                if (affected == 0)
                    affected = await unitOfWork.PartnerEquip.AddPartnerEquip(player.Id, i, player.PartnerEquips[i] ?? string.Empty);

                if (affected != 1)
                    return Result.PersistenceFailure($"Failed to persist partner equip slot '{i}'.");
            }

            return Result.Success();
        }
    }
}
