using LMSPro.Api.Dtos;

namespace LMSPro.Api.Services;

public interface IAuthService
{
    Task<long> RegisterAsync(RegisterDto registerDto);
    Task<LoginResponseDto> LoginAsync(LoginDto loginDto);
    Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto);
    Task LogoutAsync(RefreshTokenRequestDto refreshTokenRequestDto);
}