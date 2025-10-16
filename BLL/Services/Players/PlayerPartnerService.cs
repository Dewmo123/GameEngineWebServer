using BLL.Caching;
using BLL.DTOs;

namespace BLL.Services.Players
{
    public class PlayerPartnerService : IPlayerPartnerService
    {
        public void LevelUpPartner(Player player, string partnerName, int level)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (!player.Partners.ContainsKey(partnerName))
                    return;
                PartnerDTO dto = player.Partners[partnerName];
                dto.Level += level;
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }

        public bool ChangePartner(Player player, string partnerName, int amount)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (!DefaultSetting.partners.Contains(partnerName))
                    return false;
                if (!player.Partners.ContainsKey(partnerName)) 
                    return false;
                player.Partners[partnerName].Amount += amount;
                return true;
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }

        public bool EquipPartner(Player player, int idx, string? partnerName)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (idx < 0 || idx >= player.PartnerEquips.Length)
                    return false;
                if (!string.IsNullOrEmpty(partnerName) && !player.Partners.ContainsKey(partnerName))
                    return false;
                player.PartnerEquips[idx] = partnerName;
                return true;
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }

        public void AddPartner(Player player, PartnerDTO partner)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (player.Partners.ContainsKey(partner.PartnerName) || !DefaultSetting.partners.Contains(partner.PartnerName))
                    return;
                player.Partners.Add(partner.PartnerName, partner);
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }
    }
}
