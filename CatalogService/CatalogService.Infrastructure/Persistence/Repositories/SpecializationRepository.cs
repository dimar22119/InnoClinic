using CatalogService.Application.Interfaces.Repository;
using CatalogService.Application.Models;
using CatalogService.Domain.Common;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Infrastructure.Persistence.Repositories
{
    public class SpecializationRepository(CatalogDbContext db) : ISpecializationRepository
    {
        public async Task<Specialization?> GetByIdAsync(Guid id)
            => await db.Specializations.FindAsync(id);

        public async Task AddAsync(Specialization specialization)
        {
            db.Specializations.Add(specialization);
            await db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Specialization specialization)
        {
            db.Specializations.Update(specialization);
            await db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Specialization specialization)
        {
            db.Specializations.Remove(specialization);
            await db.SaveChangesAsync();
        }

        public async Task<PagedList<Specialization>> GetPagedAsync(PaginationParams paginationParams)
        {
            var query = db.Specializations.AsNoTracking();

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(s => s.Id)
                .Skip((paginationParams.PageNumber - 1) * paginationParams.PageSize)
                .Take(paginationParams.PageSize)
                .ToListAsync();

            return new PagedList<Specialization>(items, totalCount);

        }
    }

}
