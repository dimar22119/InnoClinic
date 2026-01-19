using CatalogService.Application.Interfaces.Repository;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Infrastructure.Persistence.Repositories
{
    public class SpecializationRepository(CatalogDbContext db) : ISpecializationRepository
    {
        public async Task<Specialization?> GetByIdAsync(Guid id)
            => await db.Specializations.FindAsync(id);

        public async Task<IReadOnlyList<Specialization>> GetAllAsync()
            => await db.Specializations.AsNoTracking().ToListAsync();

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
    }

}
