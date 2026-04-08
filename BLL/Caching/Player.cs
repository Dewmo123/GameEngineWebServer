using BLL.Common.Results;
using BLL.Domain.Players;
using DAL.VOs;

namespace BLL.Caching
{
    public class Player
    {
        private readonly ReaderWriterLockSlim _lock = new();
        private PlayerChapterState _chapter;
        private Dictionary<StatType, int> _stats;
        private Dictionary<GoodsType, int> _goods;
        private Dictionary<string, PlayerSkillState> _skills;
        private Dictionary<string, PlayerPartnerState> _partners;
        private string?[] _skillEquips;
        private string?[] _partnerEquips;

        public Player(PlayerState state)
        {
            PlayerState normalized = state.Clone();
            normalized.ApplyDefaults();

            Id = normalized.Id;
            _chapter = normalized.Chapter;
            _stats = normalized.Stats;
            _goods = normalized.Goods;
            _skills = normalized.Skills;
            _partners = normalized.Partners;
            _skillEquips = normalized.SkillEquips;
            _partnerEquips = normalized.PartnerEquips;
        }

        public int Id { get; }

        public PlayerState GetSnapshot()
        {
            _lock.EnterReadLock();
            try
            {
                return new PlayerState
                {
                    Id = Id,
                    Chapter = _chapter.Clone(),
                    Stats = new Dictionary<StatType, int>(_stats),
                    Goods = new Dictionary<GoodsType, int>(_goods),
                    Skills = _skills.ToDictionary(item => item.Key, item => item.Value.Clone(), StringComparer.Ordinal),
                    Partners = _partners.ToDictionary(item => item.Key, item => item.Value.Clone(), StringComparer.Ordinal),
                    SkillEquips = _skillEquips.ToArray(),
                    PartnerEquips = _partnerEquips.ToArray()
                };
            }
            finally
            {
                _lock.ExitReadLock();
            }
        }

        public Result LevelUpStat(StatType stat, int level)
        {
            if (level <= 0)
                return Result.Invalid("Stat level increase must be positive.");

            _lock.EnterWriteLock();
            try
            {
                if (!_stats.ContainsKey(stat))
                    return Result.NotFound("Stat does not exist.");

                _stats[stat] += level;
                return Result.Success();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result ChangeGoods(GoodsType goodsType, int amount)
        {
            _lock.EnterWriteLock();
            try
            {
                if (!_goods.ContainsKey(goodsType))
                    return Result.NotFound("Goods does not exist.");

                if (_goods[goodsType] + amount < 0)
                    return Result.Invalid("Goods amount cannot be negative.");

                _goods[goodsType] += amount;
                return Result.Success();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result LevelUpSkill(string? skillName, int level)
        {
            if (string.IsNullOrWhiteSpace(skillName))
                return Result.Invalid("Skill name is required.");

            _lock.EnterWriteLock();
            try
            {
                if (!_skills.TryGetValue(skillName, out PlayerSkillState? skill))
                    return Result.NotFound("Skill does not exist.");

                if (skill.Level + level < 0)
                    return Result.Invalid("Skill level cannot be negative.");

                skill.Level += level;
                return Result.Success();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result AddSkillAmount(string? skillName, int amount)
        {
            if (string.IsNullOrWhiteSpace(skillName))
                return Result.Invalid("Skill name is required.");

            if (!DefaultSetting.SkillNames.Contains(skillName))
                return Result.Invalid("Unknown skill.");

            _lock.EnterWriteLock();
            try
            {
                if (_skills.TryGetValue(skillName, out PlayerSkillState? skill))
                {
                    if (skill.Amount + amount < 0)
                        return Result.Invalid("Skill amount cannot be negative.");

                    skill.Amount += amount;
                }
                else
                {
                    if (amount < 0)
                        return Result.Invalid("Skill amount cannot be negative.");

                    _skills[skillName] = new PlayerSkillState
                    {
                        Name = skillName,
                        Amount = amount
                    };
                }

                return Result.Success();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result EquipSkill(int idx, string? skillName)
        {
            _lock.EnterWriteLock();
            try
            {
                if (idx < 0 || idx >= _skillEquips.Length)
                    return Result.Invalid("Skill equip index is out of range.");

                if (!string.IsNullOrWhiteSpace(skillName) && !_skills.ContainsKey(skillName))
                    return Result.NotFound("Skill does not exist.");

                _skillEquips[idx] = string.IsNullOrWhiteSpace(skillName) ? null : skillName;
                return Result.Success();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result SetSkillProgress(string? skillName, int amount, int upgrade)
        {
            if (string.IsNullOrWhiteSpace(skillName))
                return Result.Invalid("Skill name is required.");

            if (amount < 0 || upgrade < 0)
                return Result.Invalid("Skill amount and upgrade must be non-negative.");

            _lock.EnterWriteLock();
            try
            {
                if (!_skills.TryGetValue(skillName, out PlayerSkillState? skill))
                    return Result.NotFound("Skill does not exist.");

                skill.Amount = amount;
                skill.Upgrade = upgrade;
                return Result.Success();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result LevelUpPartner(string? partnerName, int level)
        {
            if (string.IsNullOrWhiteSpace(partnerName))
                return Result.Invalid("Partner name is required.");

            _lock.EnterWriteLock();
            try
            {
                if (!_partners.TryGetValue(partnerName, out PlayerPartnerState? partner))
                    return Result.NotFound("Partner does not exist.");

                if (partner.Level + level < 0)
                    return Result.Invalid("Partner level cannot be negative.");

                partner.Level += level;
                return Result.Success();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result AddPartnerAmount(string? partnerName, int amount)
        {
            if (string.IsNullOrWhiteSpace(partnerName))
                return Result.Invalid("Partner name is required.");

            if (!DefaultSetting.PartnerNames.Contains(partnerName))
                return Result.Invalid("Unknown partner.");

            _lock.EnterWriteLock();
            try
            {
                if (_partners.TryGetValue(partnerName, out PlayerPartnerState? partner))
                {
                    if (partner.Amount + amount < 0)
                        return Result.Invalid("Partner amount cannot be negative.");

                    partner.Amount += amount;
                }
                else
                {
                    if (amount < 0)
                        return Result.Invalid("Partner amount cannot be negative.");

                    _partners[partnerName] = new PlayerPartnerState
                    {
                        Name = partnerName,
                        Amount = amount
                    };
                }

                return Result.Success();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result EquipPartner(int idx, string? partnerName)
        {
            _lock.EnterWriteLock();
            try
            {
                if (idx < 0 || idx >= _partnerEquips.Length)
                    return Result.Invalid("Partner equip index is out of range.");

                if (!string.IsNullOrWhiteSpace(partnerName) && !_partners.ContainsKey(partnerName))
                    return Result.NotFound("Partner does not exist.");

                _partnerEquips[idx] = string.IsNullOrWhiteSpace(partnerName) ? null : partnerName;
                return Result.Success();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result SetPartnerProgress(string? partnerName, int amount, int upgrade)
        {
            if (string.IsNullOrWhiteSpace(partnerName))
                return Result.Invalid("Partner name is required.");

            if (amount < 0 || upgrade < 0)
                return Result.Invalid("Partner amount and upgrade must be non-negative.");

            _lock.EnterWriteLock();
            try
            {
                if (!_partners.TryGetValue(partnerName, out PlayerPartnerState? partner))
                    return Result.NotFound("Partner does not exist.");

                partner.Amount = amount;
                partner.Upgrade = upgrade;
                return Result.Success();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result<PlayerChapterState> ChangeChapter(int chapterDelta)
        {
            if (chapterDelta <= 0)
                return Result<PlayerChapterState>.Invalid("Chapter change must be positive.");

            _lock.EnterWriteLock();
            try
            {
                _chapter.Chapter += chapterDelta;
                _chapter.Stage = 1;
                _chapter.EnemyCount = 0;
                return Result<PlayerChapterState>.Success(_chapter.Clone());
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result<PlayerChapterState> ChangeStage(int stageDelta)
        {
            _lock.EnterWriteLock();
            try
            {
                if (_chapter.Stage + stageDelta <= 0)
                    return Result<PlayerChapterState>.Invalid("Stage must remain positive.");

                _chapter.Stage += stageDelta;
                _chapter.EnemyCount = 0;
                return Result<PlayerChapterState>.Success(_chapter.Clone());
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }

        public Result RegisterEnemyDeath(int count)
        {
            if (count <= 0)
                return Result.Invalid("Enemy count must be positive.");

            _lock.EnterWriteLock();
            try
            {
                _chapter.EnemyCount += count;
                return Result.Success();
            }
            finally
            {
                _lock.ExitWriteLock();
            }
        }
    }
}
