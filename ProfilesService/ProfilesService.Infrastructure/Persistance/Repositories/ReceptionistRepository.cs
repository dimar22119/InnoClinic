using Microsoft.EntityFrameworkCore;
using ProfilesService.Application.Interfaces.Repositories;
using ProfilesService.Domain.Entities;

namespace ProfilesService.Infrastructure.Persistance.Repositories
{
    public class ReceptionistRepository(ProfilesDbContext context) : IReceptionistRepository
    {
        public async Task<Receptionist?> GetByIdAsync(Guid id)
            => await context.Receptionists.FindAsync(id);

        public async Task<IEnumerable<Receptionist>> GetAllAsync()
            => await context.Receptionists.ToListAsync();

        public async Task AddAsync(Receptionist receptionist)
        {
            await context.Receptionists.AddAsync(receptionist);
            await context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Receptionist receptionist)
        {
            context.Receptionists.Update(receptionist);
            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var receptionist = await context.Receptionists.FindAsync(id);
            if (receptionist != null)
            {
                context.Receptionists.Remove(receptionist);
                await context.SaveChangesAsync();
            }
        }
    }
}
