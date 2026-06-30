using LMSPro.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSPro.Api.FluentApies;

public class TeacherCourseConfiguration : IEntityTypeConfiguration<TeacherCourse>
{
    public void Configure(EntityTypeBuilder<TeacherCourse> builder)
    {
        builder.HasKey(x => x.TeacherCourseId);

        builder.HasOne(x => x.Teacher)
            .WithMany(t => t.TeacherCourses)
            .HasForeignKey(x => x.TeacherId);
    }
}
