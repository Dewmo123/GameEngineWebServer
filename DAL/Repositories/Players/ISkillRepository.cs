using DAL.VOs;

namespace DAL.Repositories.Players
{
    public interface ISkillRepository
    {
        Task<int> AddSkill(int id,string skillName, int level,int upgrade, int amount);
        Task<int> UpdateSkill(int id, string skillName, int level,int upgrade, int amount);
        Task<List<SkillVO>> GetAllSkills(int id);
    }
}
