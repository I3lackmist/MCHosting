using Microsoft.AspNetCore.Mvc;

using CMS.Domain.DTOs;
using CMS.Logic.Services;

namespace CMS.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase {
    private readonly ILogger<UserController> _logger;
    private UsersService _usersService;

    public UserController(ILogger<UserController> logger, UsersService usersService) {
        _logger = logger;
        _usersService = usersService;
    }

    [HttpPost("login")]
    public IActionResult LoginUser([FromBody] LoginUserRequestDTO request) {
        try {
            _usersService.LoginUser(request);
            
            return StatusCode(200, new SimpleResponse("Logged in."));
        }
        catch (UnauthorizedAccessException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        catch (ArgumentException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }
        
    }

    [HttpPost("register")]
    public IActionResult RegisterUser([FromBody] RegisterUserRequestDTO request) {
        try {
            _usersService.RegisterUser(request);

            return StatusCode(200, new SimpleResponse("Registered."));
        }
        catch (ArgumentException exception) {
            return StatusCode(400, new SimpleResponse(exception.Message));
        }

    }
}
