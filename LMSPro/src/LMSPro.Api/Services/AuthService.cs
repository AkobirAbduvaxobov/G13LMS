using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class AuthService : IAuthService
{
    private readonly IBaseRepository<User> UserRepository;
    private readonly IBaseRepository<Password> PasswordRepository;

    public AuthService(IBaseRepository<User> userRepository, IBaseRepository<Password> passwordRepository)
    {
        UserRepository = userRepository;
        PasswordRepository = passwordRepository;
    }

    public Task<LoginResponseDto> LoginAsync(LoginDto loginDto)
    {
        throw new NotImplementedException();
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
