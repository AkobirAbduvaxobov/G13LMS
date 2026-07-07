using LMSPro.Api.Dtos;
using LMSPro.Api.Entities;

namespace LMSPro.Api.Services;

public interface IUserService
{
    Task<List<UserGetDto>> GetAllAsync();
    Task<UserGetDto> GetByIdAsync(long userId);
    Task DeleteAsync(long userId);
    Task SetRoleAsync(long userId, UserRole userRole);

    /// <summary>
    /// Updates the user information based on the provided userId and UserUpdateDto.
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="userUpdateDto"></param>
    /// <returns></returns>
    Task UpdateAsync(long userId, UserUpdateDto userUpdateDto);
}