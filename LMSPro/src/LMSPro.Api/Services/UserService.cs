using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;
using LMSPro.Api.Repositories;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.Services;

public class UserService : IUserService
{
    private readonly IBaseRepository<User> UserRepository;
    private readonly ICurrentUserService CurrentUserService;

    public UserService(IBaseRepository<User> userRepository, ICurrentUserService currentUserService)
    {
        UserRepository = userRepository;
        CurrentUserService = currentUserService;
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

    public async Task SetRoleAsync(long userId, UserRole userRole)
    {
        var user = await UserRepository.GetAllQuery()
                                .FirstOrDefaultAsync(u => u.UserId == userId);
        if (user == null)
        {
            throw new KeyNotFoundException($"User with ID {userId} not found.");
        }

        if(user.UserId == CurrentUserService.UserId)
        {
            throw new UnauthorizedAccessException("User can not change yourself");
        }


        if (CurrentUserService.Role == UserRole.SuperAdmin)
        {
            user.Role = userRole;
        }
        else if(CurrentUserService.Role == UserRole.Admin 
            && (userRole == UserRole.Teacher || userRole == UserRole.Student)
            && (user.Role == UserRole.Teacher || user.Role == UserRole.Student))
        {
            user.Role = userRole;
        }
        else
        {
            throw new UnauthorizedAccessException("You do not have permission to change this user's role.");
        }

        await UserRepository.SaveChangesAsync();
    }

    public Task UpdateAsync(long userId, UserUpdateDto userUpdateDto)
    {
        throw new NotImplementedException();
    }
}
