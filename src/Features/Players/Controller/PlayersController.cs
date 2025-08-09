using Microsoft.AspNetCore.Mvc;
using volei_app.Features.Players.DTOs;
using volei_app.Features.Players.Services;
namespace volei_app.Features.Players;

[ApiController]
[Route("api/[controller]")]
public class PlayersController : ControllerBase
{
    private readonly IPlayerService _playerService;

    public PlayersController(IPlayerService playerService)
    {
        _playerService = playerService;
    }
    
    [HttpGet]
    public IActionResult get()
    {
        return Ok(_playerService.GetPlayers());
    }
    
    [HttpPost("confirm")]
    public async Task<IActionResult> Confirm([FromBody] ConfirmPlayerRequestDto request)
    {
        var result = await _playerService.ConfirmPresence(request.Name);

        if (result.Contains("sucesso"))
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
}