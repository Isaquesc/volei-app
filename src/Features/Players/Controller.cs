using Microsoft.AspNetCore.Mvc;
using volei_app.Features.Players.DTOs;
using volei_app.Features.Players.Services;
namespace volei_app.Features.Players;

[ApiController]
[Route("api/players")]
public class Controller : ControllerBase
{
    private readonly IPlayerUseCase _playerUseCase;

    public Controller(IPlayerUseCase playerUseCase)
    {
        _playerUseCase = playerUseCase;
    }
    
    [HttpGet]
    public IActionResult get()
    {
        return Ok(_playerUseCase.GetPlayers());
    }
    
    [HttpPost("confirm")]
    public async Task<IActionResult> Confirm([FromBody] ConfirmPlayerRequestDto request)
    {
        var result = await _playerUseCase.ConfirmPresence(request.Name);

        if (result.Contains("sucesso"))
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
}