using LMSPro.Api.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LMSPro.Api.FluentApies;

public class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
{
    public void Configure(EntityTypeBuilder<Teacher> builder)
    {
        builder.HasKey(x => x.TeacherId);

        builder.HasMany(x => x.TeacherCourses)
            .WithOne(x => x.Teacher)
            .HasForeignKey(x => x.TeacherId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
