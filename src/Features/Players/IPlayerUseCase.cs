namespace volei_app.Features.Players.Services;

public interface IPlayerUseCase
{
    List<string> GetPlayers();
    Task<string> ConfirmPresence(string playerName);
}