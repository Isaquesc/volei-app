namespace volei_app.Features.Players.Services;

public interface IPlayerService
{
    List<string> GetPlayers();
    Task<string> ConfirmPresence(string playerName);
}