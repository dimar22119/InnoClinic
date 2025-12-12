using Auth.Domain.Entities;
using Auth.Domain.Repositories;
using Auth.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;

namespace Auth.Infrastructure.Repositories
{
    public class UserRepository(AuthDbContext dbContext) : IUserRepository
    {
        public async Task AddAsync(User user)
        {
            dbContext.Users.Add(user);
            await dbContext.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await dbContext.Users
                .FirstOrDefaultAsync(u => u.Email.Value == email);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await dbContext.Users
                .FirstOrDefaultAsync(u => u.Id == id);
        }
    }
}
