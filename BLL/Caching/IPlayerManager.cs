namespace BLL.Caching
{
    public interface IPlayerManager
    {
        bool TryGetPlayer(int id, out Player? player);
        Player GetOrAddPlayer(int id, Func<Player> factory);
        bool TryRemovePlayer(int id, out Player? player);
    }
}
