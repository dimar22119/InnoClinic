using CatalogService.Application.Interfaces.Repository;
using CatalogService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CatalogService.Infrastructure.Persistance.Repositories
{
    public class SpecializationRepository(CatalogDbContext db) : ISpecializationRepository
    {
        public async Task<Specialization?> GetByIdAsync(Guid id)
            => await db.Specializations.FindAsync(id);

        public async Task<IEnumerable<Specialization>> GetAllAsync()
            => await db.Specializations.ToListAsync();

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

        public async Task DeleteAsync(Guid id)
        {
            var entity = await db.Specializations.FindAsync(id);
            if (entity != null)
            {
                db.Specializations.Remove(entity);
                await db.SaveChangesAsync();
            }
        }
    }

}
