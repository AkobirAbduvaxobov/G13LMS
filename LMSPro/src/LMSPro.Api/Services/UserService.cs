using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> UserRepository;

    public UserService(IBaseRepository<User> userRepository)
    {
        UserRepository = userRepository;
    }

    public Task DeleteAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<UserGetDto>> GetAllAsync()
    {
        var users = await UserRepository.GetAllQuery().ToListAsync();

        var dtos = users.Select(u => new UserGetDto()
        {
            UserId = u.UserId,
            UserName = u.UserName,
            Email = u.Email,
            Role = u.Role,
            FirstName = u.FirstName,
            LastName = u.LastName,
            EmailConfirmed = u.EmailConfirmed,
            CreatedAt = u.CreatedAt
        }).ToList();

        return dtos;
    }

    public Task<UserGetDto> GetByIdAsync(long userId)
    {
        throw new NotImplementedException();
    }

    public Task SetRoleAsync(long userId, UserRole userRole)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(long userId, UserUpdateDto userUpdateDto)
    {
        throw new NotImplementedException();
    }
}
