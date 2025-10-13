using DAL.VOs;

namespace DAL.Repositories.Players
{
    public interface IPartnerRepository
    {
        Task<int> AddPartner(int id,string partnerName, int level,int upgrade, int amount);
        Task<int> UpdatePartner(int id, string partnerName, int level,int upgrade, int amount);
        Task<List<PartnerVO>> GetAllPartners(int id);
    }
}
