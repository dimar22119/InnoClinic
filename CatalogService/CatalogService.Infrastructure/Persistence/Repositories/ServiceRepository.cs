using CatalogService.Application.Interfaces.Repository;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository(CatalogDbContext db) : IServiceRepository
    {
        public async Task<Service?> GetByIdAsync(Guid id)
            => await db.Services.FindAsync(id);

        public async Task<IReadOnlyList<Service>> GetAllAsync()
            => await db.Services.AsNoTracking().ToListAsync();

        public async Task AddAsync(Service service)
        {
            db.Services.Add(service);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service service)
        {
            db.Services.Update(service);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Service service)
        {
            db.Services.Remove(service);
            await db.SaveChangesAsync();
        }
    }

}
