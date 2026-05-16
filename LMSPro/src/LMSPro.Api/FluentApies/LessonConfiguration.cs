using LMSPro.Api.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.FluentApies;

public class LessonConfiguration
    : IEntityTypeConfiguration<Lesson>
{
    public void Configure(EntityTypeBuilder<Lesson> builder)
    {
        builder.ToTable("Lessons");

        builder.HasKey(x => x.LessonId);

        builder.Property(x => x.LessonId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Content)
            .IsRequired()
            .HasMaxLength(5000);

        builder.Property(x => x.Order)
            .IsRequired();

        builder.Property(x => x.Duration)
            .IsRequired();

        builder.HasMany(x => x.Questions)
            .WithOne(x => x.Lesson)
            .HasForeignKey(x => x.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Homeworks)
            .WithOne(x => x.Lesson)
            .HasForeignKey(x => x.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Resources)
            .WithOne(x => x.Lesson)
            .HasForeignKey(x => x.LessonId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasMany(x => x.Exams)
            .WithOne(x => x.Lesson)
            .HasForeignKey(x => x.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
