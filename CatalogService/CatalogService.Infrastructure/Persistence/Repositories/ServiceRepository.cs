using CatalogService.Application.Interfaces.Repository;
using CatalogService.Domain.Common;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository(CatalogDbContext db) : IServiceRepository
    {
        public async Task<Service?> GetByIdAsync(Guid id)
            => await db.Services.FindAsync(id);

        public async Task<PagedList<Service>> GetPagedAsync(int skip, int take)
        {
            var query = db.Services.AsNoTracking();

            var totalCount = await query.CountAsync();
            var items = await query.
                OrderBy(s => s.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return new PagedList<Service>(items, totalCount);
        }

        public void Add(Service service) => db.Services.Add(service);

        public void Update(Service service) => db.Services.Update(service);

        public void Delete(Service service) => db.Services.Remove(service);

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => await db.SaveChangesAsync(cancellationToken);
    }

}
