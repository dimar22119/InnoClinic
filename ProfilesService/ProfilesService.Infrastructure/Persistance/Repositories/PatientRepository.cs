using Microsoft.EntityFrameworkCore;
using ProfilesService.Application.Interfaces.Repositories;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Infrastructure.Persistance.Repositories
{
    public class PatientRepository(ProfilesDbContext context) : IPatientRepository
    {
        public async Task<Patient?> GetByIdAsync(Guid id)
            => await context.Patients.FindAsync(id);

        public async Task<IEnumerable<Patient>> GetAllAsync()
            => await context.Patients.ToListAsync();

        public async Task AddAsync(Patient patient)
        {
            await context.Patients.AddAsync(patient);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Patient patient)
        {
            context.Patients.Update(patient);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var patient = await context.Patients.FindAsync(id);
            if (patient != null)
            {
                context.Patients.Remove(patient);
                await context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<Patient>> GetByNameAsync(string firstName, string lastName)
        {
            return await context.Patients
                .Where(d =>
                    EF.Functions.Like(d.FirstName, $"%{firstName}%") &&
                    EF.Functions.Like(d.LastName, $"%{lastName}%"))
                .ToListAsync();
        }
    }
}
