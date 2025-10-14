using DAL.VOs;

namespace DAL.Repositories.Players.Equip
{
    public interface ISkillEquipRepository
    {
        Task<List<SkillEquipVO>> GetSkillEquips(int id);
        Task<int> AddSkillEquip(int id, int idx, string skillName);
        Task<int> UpdateSkillEquip(int id, int idx, string skillName);
    }
}
