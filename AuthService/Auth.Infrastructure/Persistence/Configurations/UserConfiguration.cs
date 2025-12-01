using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Auth.Infrastructure.Persistance.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedNever();

            builder.Property(x => x.CreatedAt)
                .IsRequired();

            builder.OwnsOne(x => x.Email, email =>
            {
                email.Property(e => e.Value)
                    .HasColumnName("Email")
                    .IsRequired()
                    .HasMaxLength(256);
            });

            builder.OwnsOne(x => x.PasswordHash, ph =>
            {
                ph.Property(p => p.Value)
                    .HasColumnName("PasswordHash")
                    .IsRequired();
            });

            builder.HasIndex("Email").IsUnique();
        }
    }
}
