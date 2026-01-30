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

        public async void Add(Specialization specialization) => db.Specializations.Add(specialization);
        public async void Update(Specialization specialization) => db.Specializations.Update(specialization);
        public async void Delete(Specialization specialization) => db.Specializations.Remove(specialization);

        public async Task<PagedList<Specialization>> GetPagedAsync(int skip, int take)
        {
            var query = db.Specializations.AsNoTracking();

            var totalCount = await query.CountAsync();
            var items = await query
                .OrderBy(s => s.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();

            return new PagedList<Specialization>(items, totalCount);

        }

        public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
            => await db.SaveChangesAsync(cancellationToken);
    }

}
