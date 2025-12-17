using Microsoft.EntityFrameworkCore;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Infrastructure.Persistance
{
    public class ProfilesDbContext(DbContextOptions<ProfilesDbContext> options) : DbContext(options)
    {
        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Receptionist> Receptionists { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProfilesDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}
