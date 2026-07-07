using LMSPro.Api.Entities;

namespace LMSPro.Api.Dtos;

public class UserGetDto
{
    public long UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }
}
