using volei_app.Features.Players.Models;
namespace volei_app.Features.Players.Repositories;

public interface IPlayerRepository
{
    List<string> GetPlayers();
    void AddPlayer(Player player);
    bool Exists(string playerName);
    int GetCount();
}