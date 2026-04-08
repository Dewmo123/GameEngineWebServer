using DAL.Repositories.Authorizes;
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
        private readonly MySqlConnection _connection;
        private readonly MySqlTransaction _transaction;
        private bool _completed;

        private IAuthorizeRepository? _authorizeRepository;
        private IRoleRepository? _roleRepository;
        private IChapterRepository? _chapterRepository;
        private IGoodsRepository? _goodsRepository;
        private IStatRepository? _statRepository;
        private ISkillRepository? _skillRepository;
        private IPartnerRepository? _partnerRepository;
        private ISkillEquipRepository? _skillEquipRepository;
        private IPartnerEquipRepository? _partnerEquipRepository;

        private UnitOfWork(MySqlConnection connection, MySqlTransaction transaction)
        {
            _connection = connection;
            _transaction = transaction;
        }

        public IAuthorizeRepository Authorize => _authorizeRepository ??= new AuthorizeRepository(_connection, _transaction);
        public IRoleRepository Role => _roleRepository ??= new RoleRepository(_connection, _transaction);
        public IChapterRepository Chapter => _chapterRepository ??= new ChapterRepository(_connection, _transaction);
        public IGoodsRepository Goods => _goodsRepository ??= new GoodsRepository(_connection, _transaction);
        public IStatRepository Stat => _statRepository ??= new StatRepository(_connection, _transaction);
        public ISkillRepository Skill => _skillRepository ??= new SkillRepository(_connection, _transaction);
        public IPartnerRepository Partner => _partnerRepository ??= new PartnerRepository(_connection, _transaction);
        public ISkillEquipRepository SkillEquip => _skillEquipRepository ??= new SkillEquipRepository(_connection, _transaction);
        public IPartnerEquipRepository PartnerEquip => _partnerEquipRepository ??= new PartnerEquipRepository(_connection, _transaction);

        public static async Task<IUnitOfWork> CreateAsync(string connectionString)
        {
            if (string.IsNullOrEmpty(connectionString))
                throw new ArgumentException("Connection string is required.", nameof(connectionString));

            MySqlConnection connection = new(connectionString);
            await connection.OpenAsync();
            MySqlTransaction transaction = (MySqlTransaction)await connection.BeginTransactionAsync();
            return new UnitOfWork(connection, transaction);
        }

        public async Task CommitAsync()
        {
            if (_completed)
                return;

            try
            {
                await _transaction.CommitAsync();
                _completed = true;
            }
            catch
            {
                await RollbackAsync();
                throw;
            }
        }

        public async Task RollbackAsync()
        {
            if (_completed)
                return;

            await _transaction.RollbackAsync();
            _completed = true;
        }

        public async ValueTask DisposeAsync()
        {
            try
            {
                if (!_completed && _transaction.Connection != null)
                    await RollbackAsync();
            }
            finally
            {
                await _transaction.DisposeAsync();
                await _connection.DisposeAsync();
            }
        }
    }
}
