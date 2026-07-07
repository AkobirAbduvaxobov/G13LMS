using LMSPro.Api.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace LMSPro.Api.FluentApies;

public class PasswordConfiguration : IEntityTypeConfiguration<Password>
{
    public void Configure(EntityTypeBuilder<Password> builder)
    {
        builder.ToTable("Passwords");

        builder.HasKey(x => x.PasswordId);

        builder.Property(x => x.PasswordHash)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Salt)
            .IsRequired()
            .HasMaxLength(100);

        builder.HasIndex(x => x.UserId)
            .IsUnique();
    }
}

