using LMSPro.Api.Configurations.Settings;
using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Exceptions;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class AuthService : IAuthService
{
    private readonly IBaseRepository<User> UserRepository;
    private readonly IBaseRepository<Password> PasswordRepository;
    private readonly IBaseRepository<RefreshToken> RefreshTokenRepository;
    private readonly ITokenService TokenService;
    private readonly JwtSettings JwtSettings;

    public AuthService(
        IBaseRepository<User> userRepository,
        IBaseRepository<Password> passwordRepository,
        IBaseRepository<RefreshToken> refreshTokenRepository,
        ITokenService tokenService,
        JwtSettings jwtSettings)
    {
        UserRepository = userRepository;
        PasswordRepository = passwordRepository;
        RefreshTokenRepository = refreshTokenRepository;
        TokenService = tokenService;
        JwtSettings = jwtSettings;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
    {
        var users = UserRepository.GetAllQuery();

        var user = await users
                    .Include(u => u.Password)
                    .FirstOrDefaultAsync(u =>
                    u.UserName == loginDto.UserNameOrEmail
                    || u.Email == loginDto.UserNameOrEmail);

        if (user == null)
        {
            throw new UnauthorizedAccessException("Invalid username or email.");
        }

        var isPasswordValid = PasswordHasher.Verify(loginDto.Password, user.Password.PasswordHash, user.Password.Salt);

        if (!isPasswordValid)
        {
            throw new UnauthorizedAccessException("Invalid password.");
        }

        var loginResponseDto = await GenerateLoginResponseAsync(user);

        return loginResponseDto;
    }

    public async Task<LoginResponseDto> RefreshTokenAsync(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        var storedToken = await RefreshTokenRepository.GetAllQuery()
            .Include(rt => rt.User)
            .FirstOrDefaultAsync(rt => rt.Token == refreshTokenRequestDto.RefreshToken);

        if (storedToken == null || !storedToken.IsActive)
        {
            throw new UnauthorizedException("Invalid or expired refresh token.");
        }

        var loginResponseDto = await GenerateLoginResponseAsync(storedToken.User);

        // Rotate: revoke the old refresh token and link it to the newly issued one.
        storedToken.RevokedAt = DateTime.UtcNow;
        storedToken.ReplacedByToken = loginResponseDto.RefreshToken;
        RefreshTokenRepository.Update(storedToken);
        await RefreshTokenRepository.SaveChangesAsync();

        return loginResponseDto;
    }

    public async Task LogoutAsync(RefreshTokenRequestDto refreshTokenRequestDto)
    {
        var storedToken = await RefreshTokenRepository.GetAllQuery()
            .FirstOrDefaultAsync(rt => rt.Token == refreshTokenRequestDto.RefreshToken);

        if (storedToken == null)
        {
            throw new NotFoundException("Refresh token not found.");
        }

        if (storedToken.IsActive)
        {
            storedToken.RevokedAt = DateTime.UtcNow;
            RefreshTokenRepository.Update(storedToken);
            await RefreshTokenRepository.SaveChangesAsync();
        }
    }

    private async Task<LoginResponseDto> GenerateLoginResponseAsync(User user)
    {
        var userGetDto = new UserGetDto()
        {
            UserId = user.UserId,
            UserName = user.UserName,
            Email = user.Email,
            Role = user.Role,
            FirstName = user.FirstName,
            LastName = user.LastName,
            EmailConfirmed = user.EmailConfirmed,
            CreatedAt = user.CreatedAt
        };

        var accessToken = TokenService.GetToken(userGetDto);
        var refreshTokenValue = TokenService.GenerateRefreshToken();

        var refreshToken = new RefreshToken()
        {
            Token = refreshTokenValue,
            UserId = user.UserId,
            CreatedAt = DateTime.UtcNow,
            ExpiresAt = DateTime.UtcNow.AddDays(JwtSettings.RefreshTokenLifetimeDays),
        };

        await RefreshTokenRepository.AddAsync(refreshToken);
        await RefreshTokenRepository.SaveChangesAsync();

        return new LoginResponseDto()
        {
            AccessToken = accessToken,
            RefreshToken = refreshTokenValue,
            TokenType = "Bearer",
            Expires = JwtSettings.Lifetime,
        };
    }

    public async Task<long> RegisterAsync(RegisterDto registerDto)
    {
        var tupleFromHasher = PasswordHasher.Hasher(registerDto.Password);
        

        var user = new User()
        {
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            UserName = registerDto.UserName,
            Email = registerDto.Email,
            Role = UserRole.Student,
            CreatedAt = DateTime.UtcNow,
            EmailConfirmed = false,
        };

        await UserRepository.AddAsync(user);
        await UserRepository.SaveChangesAsync();

        var password = new Password()
        {
            PasswordHash = tupleFromHasher.Item1,
            Salt = tupleFromHasher.Item2,
            CreatedTime = DateTime.UtcNow,
            ModifiedTime = DateTime.UtcNow,
            UserId = user.UserId,
        };

        await PasswordRepository.AddAsync(password);
        await PasswordRepository.SaveChangesAsync();


        return user.UserId;
    }
}
