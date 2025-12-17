using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfilesService.Domain.Common;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Infrastructure.Persistance.Configurations
{
    public class ReceptionistConfiguration : IEntityTypeConfiguration<Receptionist>
    {
        public void Configure(EntityTypeBuilder<Receptionist> builder)
        {
            builder.ToTable("Receptionists");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.AccountId)
                   .IsRequired();

            builder.Property(r => r.FirstName)
                   .IsRequired()
                   .HasMaxLength(FieldConstraints.FirstNameMaxLength);

            builder.Property(r => r.LastName)
                   .IsRequired()
                   .HasMaxLength(FieldConstraints.LastNameMaxLength);
        }
    }
}
