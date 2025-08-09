using Microsoft.AspNetCore.SignalR;
using volei_app.Features.Hubs;
using volei_app.Features.Players.Models;
using volei_app.Features.Players.Repositories;
namespace volei_app.Features.Players.Services;

public class PlayerService : IPlayerService
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IHubContext<VoleiHub> _hubContext;

    public PlayerService(IPlayerRepository playerRepository, IHubContext<VoleiHub> hubContext)
    {
        _playerRepository = playerRepository;
        _hubContext = hubContext;
    }

    public List<string> GetPlayers()
    {
        return _playerRepository.GetPlayers();
    }

    public async Task<string> ConfirmPresence(string playerName)
    {
        if (_playerRepository.GetCount() >= 12)
        {
            return "O número máximo de jogadores já foi atingido.";
        }

        if (_playerRepository.Exists(playerName))
        {
            return $"O jogador '{playerName}' já está na lista.";
        }

        var newPlayer = new Player { name = playerName.Trim() };
        _playerRepository.AddPlayer(newPlayer);
        
        await _hubContext.Clients.All.SendAsync("ReceivePlayers", _playerRepository.GetPlayers());
        
        return $"Presença de '{playerName}' confirmada com sucesso!";
    }
}