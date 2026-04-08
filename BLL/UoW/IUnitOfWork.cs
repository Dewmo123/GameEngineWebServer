using DAL.Repositories.Authorizes;
using DAL.Repositories.Players.Chapter;
using DAL.Repositories.Players.Equip;
using DAL.Repositories.Players.Goods;
using DAL.Repositories.Players.Stat;
using DAL.Repositories.Players.Unit;

namespace BLL.UoW
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        IAuthorizeRepository Authorize { get; }
        IRoleRepository Role { get; }
        IChapterRepository Chapter { get; }
        IGoodsRepository Goods { get; }
        IStatRepository Stat { get; }
        ISkillRepository Skill { get; }
        IPartnerRepository Partner { get; }
        ISkillEquipRepository SkillEquip { get; }
        IPartnerEquipRepository PartnerEquip { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
