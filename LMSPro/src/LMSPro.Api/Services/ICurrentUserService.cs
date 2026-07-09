using LMSPro.Api.Entities;

namespace LMSPro.Api.Services;

public interface ICurrentUserService
{
    long? UserId { get; }
    string? UserName { get; }
    string? FirstName { get; }
    string? LastName { get; }
    string? Email { get; }
    UserRole? Role { get; }
}