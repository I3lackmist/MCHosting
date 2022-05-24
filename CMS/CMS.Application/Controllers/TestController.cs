using Microsoft.AspNetCore.Mvc;

using CMS.Domain.DTOs;

namespace CMS.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TestController : ControllerBase {
    public TestController() {}

    [HttpGet("ping")]
    public IActionResult Ping() {
        return StatusCode(200, new SimpleResponse("Pong."));
    }

    [HttpGet("pong")]
    public IActionResult Pong() {
        return StatusCode(400, new SimpleResponse("No, I say pong."));
    }
}
