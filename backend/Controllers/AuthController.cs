using Microsoft.AspNetCore.Mvc;
using plzwork.Models.Dtos;
using plzwork.Services;

namespace plzwork.Controllers;

[ApiController]
[Route("auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto registerDto)
    {
        var result = await authService.RegisterUserAsync(registerDto);

        switch (result.Status)
        {
            case RegisterUserStatus.UsernameAlreadyExists:
                return Conflict("Username already exists");
            
            case RegisterUserStatus.Success:
                return Ok("User registered successfully");
            
            default:
                return BadRequest();
        }
    }
}