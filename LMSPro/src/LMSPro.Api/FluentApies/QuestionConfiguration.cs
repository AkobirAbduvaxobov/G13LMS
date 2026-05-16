using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;
using LMSPro.Api.Entities;

namespace LMSPro.Api.FluentApies;

public class QuestionConfiguration
    : IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder.ToTable("Questions");

        builder.HasKey(x => x.QuestionId);

        builder.Property(x => x.QuestionId)
            .ValueGeneratedOnAdd();

        builder.Property(x => x.Text)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(x => x.VariantA)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.VariantB)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.VariantC)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.VariantD)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(x => x.Answer)
            .IsRequired()
            .HasMaxLength(1);
    }
}
