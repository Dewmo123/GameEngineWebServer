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
        IAuthorizeRepository Auth { get; }
        IRoleRepository Role { get; }
        IStatRepository Stat { get; }
        IGoodsRepository Goods { get; }
        ISkillRepository Skill { get; }
        IChapterRepository Chapter { get; }
        IPartnerRepository Partner { get; }
        ISkillEquipRepository SkillEquip { get; }
        Task CommitAsync();
        Task RollbackAsync();
    }
}
