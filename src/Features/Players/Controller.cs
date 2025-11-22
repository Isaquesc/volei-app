using Microsoft.AspNetCore.Mvc;
using volei_app.Features.Players.DTOs;
using volei_app.Features.Players.Services;
namespace volei_app.Features.Players;

[ApiController]
[Route("api/players")]
public class Controller : ControllerBase
{
    private readonly IPlayerUseCase _playerUseCase;
    private readonly ILogger<Controller> _logger;

    public Controller(IPlayerUseCase playerUseCase, ILogger<Controller> logger)
    {
        _playerUseCase = playerUseCase;
        _logger = logger;   
    }
    
    [HttpGet]
    public IActionResult get()
    {   
        _logger.LogInformation("GET /api/players - Listando todos os jogadores");

        var players = _playerUseCase.GetPlayers();

        _logger.LogInformation("Total de {PlayerCount} jogadores retornados", players.Count);

        return Ok(players);
    }
    
    [HttpPost("confirm")]
    public async Task<IActionResult> Confirm([FromBody] ConfirmPlayerRequestDto request)
    {
        _logger.LogInformation("POST /api/players/confirm - Iniciando confirmação de presença do jogador {PlayerName}", request.Name);
    
        var result = await _playerUseCase.ConfirmPresence(request.Name);
    
        if (result.Contains("sucesso"))
        {
            _logger.LogInformation("POST /api/players/confirm - Presença confirmada com sucesso para {PlayerName}", request.Name);
            return Ok(result);
        }
    
        _logger.LogWarning("POST /api/players/confirm - Falha ao confirmar presença para {PlayerName}: {Result}", request.Name, result);
        return BadRequest(result);
    }
    
}