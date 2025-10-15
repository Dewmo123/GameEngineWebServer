using DAL.Repositories;
using DAL.Repositories.Authorizes;
using DAL.Repositories.Players;
using DAL.Repositories.Players.Chapter;
using DAL.Repositories.Players.Equip;
using DAL.Repositories.Players.Goods;
using DAL.Repositories.Players.Stat;
using DAL.Repositories.Players.Unit;
using MySql.Data.MySqlClient;

namespace BLL.UoW
{
    public class UnitOfWork : IUnitOfWork
    {
        static string connectionString = "Server=127.0.0.1;Port=3306;Database=summer_db;Uid=root;Pwd=1652;Pooling=true";
        private IAuthorizeRepository? _auth;
        private IRoleRepository? _role;
        private IStatRepository? _stat;
        private IGoodsRepository? _goods;
        private ISkillRepository? _skill;
        private IChapterRepository? _chapter;
        private IPartnerRepository? _partner;
        private ISkillEquipRepository? _skilEquip;
        private IPartnerEquipRepository? _partnerEquip;
        public IAuthorizeRepository Auth => _auth ??= new AuthorizeRepository(_connection, _transaction);
        public IRoleRepository Role => _role ??= new RoleRepository(_connection, _transaction);
        public IStatRepository Stat => _stat ??= new StatRepository(_connection, _transaction);
        public IGoodsRepository Goods => _goods ??= new GoodsRepository(_connection, _transaction);
        public ISkillRepository Skill => _skill ??= new SkillRepository(_connection,_transaction);
        public IChapterRepository Chapter => _chapter ??=new ChapterRepository(_connection,_transaction);
        public IPartnerRepository Partner => _partner ??= new PartnerRepository(_connection,_transaction);
        public ISkillEquipRepository SkillEquip =>_skilEquip ??= new SkillEquipRepository(_connection,_transaction);
        public IPartnerEquipRepository PartnerEquip => _partnerEquip ??= new PartnerEquipRepository(_connection,_transaction);

        private MySqlConnection _connection;
        private MySqlTransaction _transaction;
        private Dictionary<Type, Repository> _repos;
        private UnitOfWork(MySqlConnection connection, MySqlTransaction transaction)
        {
            _repos = new();
            _connection = connection;
            _transaction = transaction;
        }
        public static async Task<IUnitOfWork> CreateUoWAsync()
        {
            var connection = new MySqlConnection(connectionString);
            await connection.OpenAsync();
            var transaction = await connection.BeginTransactionAsync();
            return new UnitOfWork(connection, transaction);
        }
        public async Task CommitAsync()
        {
            try
            {
                await _transaction.CommitAsync();
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            await _transaction.RollbackAsync();
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (_transaction.Connection != null)
                    await CommitAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                await _connection.DisposeAsync();
            }
        }
    }
}
