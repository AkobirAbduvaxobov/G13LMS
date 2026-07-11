using LMSPro.Api.Dtos;
using LMSPro.Api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LMSPro.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService AuthService;

    public AuthController(IAuthService authService)
    {
        AuthService = authService;
    }

    [HttpPost("register")]
    public async Task<long> Register(RegisterDto registerDto)
    {
        var userId = await AuthService.RegisterAsync(registerDto);
        return userId;
    }

    [HttpPost("login")]
    public async Task<LoginResponseDto> Login(LoginDto loginDto)
    {
        var token = await AuthService.LoginAsync(loginDto);
        return token;
    }

    [HttpPost("refresh-token")]
    public async Task<LoginResponseDto> RefreshToken(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        var token = await AuthService.RefreshTokenAsync(refreshTokenRequestDto);
        return token;
    }

    [HttpPost("logout")]
    public async Task<IActionResult> Logout(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        await AuthService.LogoutAsync(refreshTokenRequestDto);
        return Ok(new { message = "Logged out successfully." });
    }
}
