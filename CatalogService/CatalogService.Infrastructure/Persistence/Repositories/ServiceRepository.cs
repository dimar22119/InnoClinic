using CatalogService.Application.Interfaces.Repository;
using CatalogService.Application.Models;
using CatalogService.Domain.Common;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Persistence.Repositories
{
    public class ServiceRepository(CatalogDbContext db) : IServiceRepository
    {
        public async Task<Service?> GetByIdAsync(Guid id)
            => await db.Services.FindAsync(id);

        public async Task<PagedList<Service>> GetPagedAsync(PaginationParams paginationParams)
        {
            var query = db.Services.AsNoTracking();

            var totalCount = await query.CountAsync();
            var items = await query.
                OrderBy(s => s.Id)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PagedList<Service>(items, totalCount);
        }

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
