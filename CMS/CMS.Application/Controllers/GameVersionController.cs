using Microsoft.AspNetCore.Mvc;

using CMS.Domain.DTOs;
using CMS.Domain.Types;
using CMS.Logic.Services;

namespace CMS.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameVersionController : ControllerBase {
    private readonly ILogger<GameVersionController> _logger;
    private GameVersionService _gameVersionService;

    public GameVersionController(ILogger<GameVersionController> logger, GameVersionService gameVersionService) {
        _logger = logger;
        _gameVersionService = gameVersionService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGameVersions() {
        try {
            return StatusCode(200, await _gameVersionService.getGameVersions());
        }
        catch (Exception e) {
            return StatusCode(500, new SimpleResponse("Server error."));
        }
    }
}
