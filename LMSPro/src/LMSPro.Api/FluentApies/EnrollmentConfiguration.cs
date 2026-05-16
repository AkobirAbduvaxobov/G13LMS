using LMSPro.Api.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.FluentApies;

public class EnrollmentConfiguration
    : IEntityTypeConfiguration<Enrollment>
{
    public void Configure(EntityTypeBuilder<Enrollment> builder)
    {
        builder.ToTable("Enrollments");

        builder.HasKey(x => x.EnrollmentId);

        builder.Property(x => x.EnrollmentId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.EnrolledAt);

        builder.HasIndex(x => new
        {
            x.StudentId,
            x.CourseId
        }).IsUnique();
    }
}
