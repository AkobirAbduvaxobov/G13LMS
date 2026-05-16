using LMSPro.Api.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.FluentApies;

public class ResourceConfiguration
    : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder.ToTable("Resources");

        builder.HasKey(x => x.ResourceId);

        builder.Property(x => x.ResourceId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Url)
            .IsRequired()
            .HasMaxLength(1000);

        builder.Property(x => x.Type)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasOne(x => x.Lesson)
            .WithMany(x => x.Resources)
            .HasForeignKey(x => x.LessonId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
