namespace LMSPro.Api.Dtos;

public class StudentGetDto
{
    public long StudentId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public DateTime RegisteredAt { get; set; }
}
