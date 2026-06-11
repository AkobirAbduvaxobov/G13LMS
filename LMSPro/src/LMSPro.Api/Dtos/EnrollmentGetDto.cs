namespace LMSPro.Api.Dtos;

public class EnrollmentGetDto
{
    public long EnrollmentId { get; set; }
    public DateTime EnrolledAt { get; set; }
    public long StudentId { get; set; }
    public string? StudentFirstName { get; set; }
    public string? StudentLastName { get; set; }
    public long CourseId { get; set; }
    public string? CourseTitle { get; set; }
}
