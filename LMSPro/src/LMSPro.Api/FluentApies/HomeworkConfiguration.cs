using LMSPro.Api.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.FluentApies;

public class HomeworkConfiguration
    : IEntityTypeConfiguration<Homework>
{
    public void Configure(EntityTypeBuilder<Homework> builder)
    {
        builder.ToTable("Homeworks");

        builder.HasKey(x => x.HomeworkId);

        builder.Property(x => x.HomeworkId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.Description)
            .IsRequired()
            .HasMaxLength(3000);
    }
}
