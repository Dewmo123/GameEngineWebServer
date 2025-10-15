using DAL.VOs;

namespace DAL.Repositories.Players.Equip
{
    public interface IPartnerEquipRepository
    {
        Task<List<PartnerEquipVO>> GetPartnerEquips(int id);
        Task<int> AddPartnerEquip(int id, int idx, string skillName);
        Task<int> UpdatePartnerEquip(int id, int idx, string skillName);
    }
}
