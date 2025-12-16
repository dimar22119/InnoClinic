using Microsoft.EntityFrameworkCore;
using ProfilesService.Application.Interfaces.Repositories;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Infrastructure.Persistance.Repositories
{
    public class DoctorRepository(ProfilesDbContext context) : IDoctorRepository
    {
        public async Task<Doctor?> GetByIdAsync(Guid id)
            => await context.Doctors.FindAsync(id);

        public async Task<IEnumerable<Doctor>> GetAllAsync()
            => await context.Doctors.ToListAsync();

        public async Task AddAsync(Doctor doctor)
        {
            await context.Doctors.AddAsync(doctor);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Doctor doctor)
        {
            context.Doctors.Update(doctor);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var doctor = await context.Doctors.FindAsync(id);
            if (doctor != null)
            {
                context.Doctors.Remove(doctor);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Doctor>> GetByNameAsync(string firstName, string lastName)
        {
            return await context.Doctors
                .Where(d =>
                    EF.Functions.Like(d.FirstName, $"%{firstName}%") &&
                    EF.Functions.Like(d.LastName, $"%{lastName}%"))
                .ToListAsync();
        }
    }
}
