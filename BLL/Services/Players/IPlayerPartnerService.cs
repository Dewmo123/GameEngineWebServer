using BLL.Caching;
using BLL.DTOs;

namespace BLL.Services.Players
{
    public interface IPlayerPartnerService
    {
        bool LevelUpPartner(Player player, string partnerName, int level);
        bool AddPartnerAmount(Player player, string partnerName, int amount);
        bool EquipPartner(Player player, int idx, string? partnerName);
        void AddPartner(Player player, PartnerDTO partner);
    }
}
