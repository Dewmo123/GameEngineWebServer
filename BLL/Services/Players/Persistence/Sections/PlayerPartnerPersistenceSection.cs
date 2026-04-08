using AutoMapper;
using BLL.DTOs;
using BLL.Services.Players.Persistence;
using BLL.UoW;
using DAL.Repositories.Players.Unit;
using DAL.VOs;

namespace BLL.Services.Players.Persistence.Sections
{
    public class PlayerPartnerPersistenceSection : IPlayerPersistenceSection
    {
        private readonly IMapper _mapper;

        public PlayerPartnerPersistenceSection(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task LoadAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            IPartnerRepository partnerRepository = unitOfWork.GetRepository<IPartnerRepository, PartnerRepository>();
            List<PartnerVO> partners = await partnerRepository.GetAllPartners(playerId);
            List<PartnerDTO> partnerDtos = _mapper.Map<List<PartnerVO>, List<PartnerDTO>>(partners);

            player.Partners = partnerDtos
                .Where(item => !string.IsNullOrEmpty(item.PartnerName))
                .ToDictionary(item => item.PartnerName!, item => item);
        }

        public async Task<bool> SaveAsync(int playerId, PlayerDTO player, IUnitOfWork unitOfWork)
        {
            IPartnerRepository partnerRepository = unitOfWork.GetRepository<IPartnerRepository, PartnerRepository>();
            foreach ((string partnerName, PartnerDTO partner) in player.Partners)
            {
                int affected = await partnerRepository.UpdatePartner(playerId, partnerName, partner.Level, partner.Upgrade, partner.Amount);
                if (affected == 0)
                    affected = await partnerRepository.AddPartner(playerId, partnerName, partner.Level, partner.Upgrade, partner.Amount);

                if (affected != 1)
                    return false;
            }

            return true;
        }
    }
}
