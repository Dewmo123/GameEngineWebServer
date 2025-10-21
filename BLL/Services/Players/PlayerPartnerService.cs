using BLL.Caching;
using BLL.DTOs;

namespace BLL.Services.Players
{
    public class PlayerPartnerService : IPlayerPartnerService
    {
        public bool LevelUpPartner(Player player, string partnerName, int level)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (!player.Partners.ContainsKey(partnerName))
                    return false;
                if (player.Partners[partnerName].Level + level < 0)
                    return false;
                PartnerDTO dto = player.Partners[partnerName];
                dto.Level += level;
                return true;
            }
            finally
            {
                player.rwLock.ExitWriteLock();
            }
        }

        public bool AddPartnerAmount(Player player, string partnerName, int amount)
        {
            try
            {
                player.rwLock.EnterWriteLock();
                if (!DefaultSetting.partners.Contains(partnerName))
                    return false;
                if (player.Partners.ContainsKey(partnerName))
                    player.Partners[partnerName].Amount += amount;
                else
                    player.Partners.Add(partnerName, new()
                    {
                        Amount = amount,
                        PartnerName = partnerName
                    });
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
                Console.WriteLine(partnerName);
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
