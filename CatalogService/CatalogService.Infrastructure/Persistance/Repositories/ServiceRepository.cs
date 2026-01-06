using CatalogService.Application.Interfaces.Repository;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Persistance.Repositories
{
    public class ServiceRepository(CatalogDbContext db) : IServiceRepository
    {
        public async Task<Service?> GetByIdAsync(Guid id)
            => await db.Services.FindAsync(id);

        public async Task<IEnumerable<Service>> GetAllAsync()
            => await db.Services.ToListAsync();

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

        public async Task DeleteAsync(Guid id)
        {
            var entity = await db.Services.FindAsync(id);
            if (entity != null)
            {
                db.Services.Remove(entity);
                await db.SaveChangesAsync();
            }
        }
    }

}
