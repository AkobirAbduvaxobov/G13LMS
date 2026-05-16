namespace LMSPro.Api.Entities;

public class Enrollment
{
    public long EnrollmentId { get; set; }
    public DateTime EnrolledAt { get; set; }

    // Navigation Properties
    public Student Student { get; set; }
    public long StudentId { get; set; }

    public Course Course { get; set; }
    public long CourseId { get; set; }
}
