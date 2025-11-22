using Microsoft.AspNetCore.SignalR;
using volei_app.Features.Hubs;
using volei_app.Features.Players.Models;
using volei_app.Features.Players.Repositories;
namespace volei_app.Features.Players.Services;

public class PlayerUseCase : IPlayerUseCase
{
    private readonly IPlayerRepository _playerRepository;
    private readonly IHubContext<VoleiHub> _hubContext;

    private readonly ILogger<PlayerUseCase> _logger;

    public PlayerUseCase(IPlayerRepository playerRepository, IHubContext<VoleiHub> hubContext, ILogger<PlayerUseCase> logger)
    {
        _playerRepository = playerRepository;
        _hubContext = hubContext;
        _logger = logger;
    }

    public List<string> GetPlayers()
    {

        var players = _playerRepository.GetPlayers();

        _logger.LogInformation("Total de {PlayerCount} jogadores encontrados", players.Count);

        return players;
    }

    public async Task<string> ConfirmPresence(string playerName)
    {
        _logger.LogInformation("Tentando confirmar presença do jogador {PlayerName}", playerName);
        var result = ValidatorPlayerConfirmation(playerName);
        
        if (result.Contains("sucesso"))
        {
            var newPlayer = new Player { name = playerName.Trim() };
            _playerRepository.AddPlayer(newPlayer);
            
            _logger.LogInformation("Jogador {PlayerName} adicionado. ", playerName);
            await _hubContext.Clients.All.SendAsync("ReceivePlayers", _playerRepository.GetPlayers());
            
            _logger.LogInformation("Presença confirmada com sucesso para {PlayerName}", playerName);
        }
        else
        {
            _logger.LogWarning("Falha ao confirmar presença para {PlayerName}: Motivo: {ValidationResult}", playerName, result);
        }
        
        return result;
    }

    private string ValidatorPlayerConfirmation(string playerName)
    {
        if (string.IsNullOrWhiteSpace(playerName))
        {
            return "Nome do jogador inválido.";
        }
    
        if (_playerRepository.GetCount() >= 12)
        {
            return "O número máximo de jogadores já foi atingido.";
        }
    
        if (_playerRepository.Exists(playerName))
        {
            return $"O jogador '{playerName}' já está na lista.";
        }
    
        return "sucesso";
    }
}