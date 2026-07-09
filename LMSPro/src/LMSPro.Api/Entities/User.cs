namespace LMSPro.Api.Entities;

public class User
{
    public long UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool EmailConfirmed { get; set; }
    public UserRole Role { get; set; }
    public DateTime CreatedAt { get; set; }

    public Password Password { get; set; }
    public ICollection<Car> Cars { get; set; }
}
