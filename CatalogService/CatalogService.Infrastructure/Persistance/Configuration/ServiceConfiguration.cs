using CatalogService.Domain.Common;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogService.Infrastructure.Persistance.Configuration
{
    public class ServiceConfiguration : IEntityTypeConfiguration<Service>
    {
        public void Configure(EntityTypeBuilder<Service> builder)
        {
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(FieldConstraints.ServiceNameMaxLength);

            builder.Property(x => x.Price)
                .HasPrecision(10, 2);

            builder.Property(x => x.Category)
                .HasConversion<int>();

            builder.HasOne<Specialization>()
                .WithMany()
                .HasForeignKey(x => x.SpecializationId);
        }
    }

}
