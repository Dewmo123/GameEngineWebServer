using AutoMapper;
using BLL.DTOs;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.Repositories.Players.Unit;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerSkillPersistenceSection : IPlayerPersistenceSection
    {
        private readonly IMapper _mapper;

        public PlayerSkillPersistenceSection(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task LoadAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            ISkillRepository skillRepository = unitOfWork.GetRepository<ISkillRepository, SkillRepository>();
            List<SkillVO> skills = await skillRepository.GetAllSkills(playerId);
            List<SkillDTO> skillDtos = _mapper.Map<List<SkillVO>, List<SkillDTO>>(skills);

            player.Skills = skillDtos
                .Where(item => !string.IsNullOrEmpty(item.SkillName))
                .ToDictionary(item => item.SkillName!, item => item);
        }

        public async Task<bool> SaveAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            ISkillRepository skillRepository = unitOfWork.GetRepository<ISkillRepository, SkillRepository>();
            foreach ((string skillName, SkillDTO skill) in player.Skills)
            {
                int affected = await skillRepository.UpdateSkill(playerId, skillName, skill.Level, skill.Upgrade, skill.Amount);
                if (affected == 0)
                    affected = await skillRepository.AddSkill(playerId, skillName, skill.Level, skill.Upgrade, skill.Amount);

                if (affected != 1)
                    return false;
            }

            return true;
        }
    }
}
