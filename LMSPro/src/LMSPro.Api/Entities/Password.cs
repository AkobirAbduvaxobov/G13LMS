namespace LMSPro.Api.Entities;

public class Password
{
    public long PasswordId { get; set; }
    public string PasswordHash { get; set; }
    public string Salt { get; set; }
    public DateTime CreatedTime { get; set; }
    public DateTime ModifiedTime { get; set; }

    public long UserId { get; set; }
    public User User { get; set; }
}
