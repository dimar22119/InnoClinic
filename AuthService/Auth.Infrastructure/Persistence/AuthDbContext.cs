using Auth.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Infrastructure.Persistance
{
    public class AuthDbContext(DbContextOptions<AuthDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AuthDbContext).Assembly);
        }
    }
}
