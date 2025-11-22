using volei_app.Features.Players.Models;
namespace volei_app.Features.Players.Repositories;

public class PlayerRepository : IPlayerRepository
{
    private readonly List<Player> _players = new();
    private const int _maxPlayers = 12;

    public List<string> GetPlayers()
    {
        return _players.Select(p => p.name).ToList();
    }

    public void AddPlayer(Player player)
    {
        _players.Add(player);
    }
    
    public bool Exists(string playerName)
    {
        return _players.Any(p => p.name.Equals(playerName, StringComparison.OrdinalIgnoreCase));
    }

    public int GetCount()
    {
        return _players.Count;
    }
}