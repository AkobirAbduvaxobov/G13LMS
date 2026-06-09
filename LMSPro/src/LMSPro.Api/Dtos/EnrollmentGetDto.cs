namespace LMSPro.Api.Dtos;

public class EnrollmentGetDto
{
    public long EnrollmentId { get; set; }
    public DateTime EnrolledAt { get; set; }
    public long StudentId { get; set; }
    public long CourseId { get; set; }
}
