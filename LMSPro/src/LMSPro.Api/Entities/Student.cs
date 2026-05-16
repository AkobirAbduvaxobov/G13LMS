namespace LMSPro.Api.Entities;

public class Student
{
    public long StudentId { get; set; }

    public string FirstName { get; set; }

    public string LastName { get; set; }

    public string Email { get; set; }

    public DateTime RegisteredAt { get; set; }

    // Navigation Properties
    public ICollection<Enrollment> Enrollments { get; set; }
}
