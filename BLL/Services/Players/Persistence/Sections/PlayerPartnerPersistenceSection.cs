using BLL.Common.Results;
using BLL.Domain.Players;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerPartnerPersistenceSection : IPlayerPersistenceSection
    {
        public async Task LoadAsync(int playerId, PlayerState player, IUnitOfWork unitOfWork)
        {
            List<PartnerVO> partners = await unitOfWork.Partner.GetAllPartners(playerId);
            player.Partners = partners
                .Where(item => !string.IsNullOrWhiteSpace(item.PartnerName))
                .ToDictionary(
                    item => item.PartnerName!,
                    item => new PlayerPartnerState
                    {
                        Name = item.PartnerName!,
                        Level = item.Level,
                        Upgrade = item.Upgrade,
                        Amount = item.Amount
                    },
                    StringComparer.Ordinal);
        }

        public async Task<Result> SaveAsync(PlayerState player, IUnitOfWork unitOfWork)
        {
            foreach ((string partnerName, PlayerPartnerState partner) in player.Partners)
            {
                int affected = await unitOfWork.Partner.UpdatePartner(player.Id, partnerName, partner.Level, partner.Upgrade, partner.Amount);
                if (affected == 0)
                    affected = await unitOfWork.Partner.AddPartner(player.Id, partnerName, partner.Level, partner.Upgrade, partner.Amount);

                if (affected != 1)
                    return Result.PersistenceFailure($"Failed to persist partner '{partnerName}'.");
            }

            return Result.Success();
        }
    }
}
