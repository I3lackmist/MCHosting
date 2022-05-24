using Microsoft.AspNetCore.Mvc;

using CMS.Domain.DTOs;

namespace CMS.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ServerFlavorController : ControllerBase {
    private readonly ILogger<ServerFlavorController> _logger;
    private ServerFlavorService _serverFlavorService;

    public ServerFlavorController(ILogger<ServerFlavorController> logger, ServerFlavorService serverFlavorService) {
        _logger = logger;
        _serverFlavorService = serverFlavorService;
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetServerFlavors() {
        try {
            return StatusCode(200, _serverFlavorService.GetServerFlavors());
        }
        catch (Exception e) {
            return StatusCode(500, new SimpleResponse("Server error."));
        }
    }
}
