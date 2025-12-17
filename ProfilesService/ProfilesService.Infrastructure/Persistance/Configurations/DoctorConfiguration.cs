using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProfilesService.Domain.Common;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Infrastructure.Persistance.Configurations
{
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.HasKey(d => d.Id);

            builder.Property(d => d.AccountId)
                    .IsRequired();

            builder.Property(d => d.SpecializationId)
                .IsRequired();

            builder.Property(d => d.FirstName)
                   .IsRequired()
                   .HasMaxLength(FieldConstraints.FirstNameMaxLength);

            builder.Property(d => d.LastName)
                   .IsRequired()
                   .HasMaxLength(FieldConstraints.LastNameMaxLength);

            builder.Property(d => d.CareerYearStart)
                   .IsRequired();

            builder.Property(d => d.Status)
                   .IsRequired()
                   .HasConversion<string>();
        }
    }
}
