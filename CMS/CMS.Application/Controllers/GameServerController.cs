using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;

using CMS.Domain.DTOs;
using CMS.Domain.Types;
using CMS.Logic.Services;

namespace CMS.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GameServerController : ControllerBase {
    private readonly ILogger<GameServerController> _logger;
    private GameServerService _gameServerService;

    public GameServerController(
        ILogger<GameServerController> logger,
        GameServerService gameServerService
        ) {
        _logger = logger;
        _gameServerService = gameServerService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGameServer([FromQuery] string gameServerName) {
        try { 
            return StatusCode(200, await _gameServerService.GetGameServer(gameServerName));
        }
        catch (UnauthorizedAccessException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (ArgumentException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateGameServer([FromBody] CreateGameServerRequestDTO request) {
        try {
            await _gameServerService.CreateGameServer(request);

            return StatusCode(200, new SimpleResponse("Server created."));
        }
        catch (UnauthorizedAccessException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (ArgumentException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (InternalServiceException exception) {
            return StatusCode(500, new SimpleResponse(exception.Message));
        }
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetUsersGameServers([FromQuery] string username) {
        try {
            return StatusCode(200, _gameServerService.GetUsersGameServers(username));
        } catch (InternalServiceException exception) {
            return StatusCode(500, new SimpleResponse(exception.Message));
        }
    }

    [HttpPut("name")]
    public async Task<IActionResult> ChangeGameServerName([FromBody] ChangeGameServerNameRequestDTO request) {
        try {
            await _gameServerService.ChangeGameServerName(request);

            return StatusCode(200, new SimpleResponse("Name changed."));
        }
        catch (UnauthorizedAccessException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (ArgumentException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (InternalServiceException exception) {
            return StatusCode(500, new SimpleResponse(exception.Message));
        }
    }

    [HttpPut("version")]
    public async Task<IActionResult> ChangeGameServerVersion([FromBody] ChangeGameServerVersionRequestDTO request) {
        try {
            await _gameServerService.ChangeGameServerVersion(request);

            return StatusCode(200, new SimpleResponse("Version changed."));
        }
        catch (UnauthorizedAccessException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (ArgumentException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (InternalServiceException exception) {
            return StatusCode(500, new SimpleResponse(exception.Message));
        }
    }

    [HttpPut("flavor")]
    public async Task<IActionResult> ChangeGameServerFlavor([FromBody] ChangeGameServerFlavorRequestDTO request) {
        try {
            await _gameServerService.ChangeGameServerFlavor(request);
            return StatusCode(200, new SimpleResponse("Flavor changed."));
        }
        catch (UnauthorizedAccessException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (ArgumentException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (InternalServiceException exception) {
            return StatusCode(500, new SimpleResponse(exception.Message));
        }
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteGameServer([FromQuery] string gameservername, [FromHeader] string requestingUserName) {
        try {
            await _gameServerService.DeleteGameServer(gameservername, requestingUserName);
            return StatusCode(200, new SimpleResponse("Server deleted."));
        }
        catch (ArgumentException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (UnauthorizedAccessException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (InternalServiceException exception) {
            return StatusCode(500, new SimpleResponse(exception.Message));
        }
    }
}
