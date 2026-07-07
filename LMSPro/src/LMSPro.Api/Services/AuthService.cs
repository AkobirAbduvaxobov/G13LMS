using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class AuthService : IAuthService
{
    private readonly IBaseRepository<User> UserRepository;
    private readonly IBaseRepository<Password> PasswordRepository;
    private readonly ITokenService TokenService;

    public AuthService(IBaseRepository<User> userRepository, IBaseRepository<Password> passwordRepository, ITokenService tokenService)
    {
        UserRepository = userRepository;
        PasswordRepository = passwordRepository;
        TokenService = tokenService;
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

        var token = TokenService.GetToken(userGetDto);

        var loginResponseDto = new LoginResponseDto()
        {
            AccessToken = token,
            RefreshToken = null,
            TokenType = "Bearer",
            Expires = 5,
        };


        return loginResponseDto;
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
