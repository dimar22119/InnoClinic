using Auth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByEmailAsync(string email);
        Task<User?> GetByIdAsync(Guid id);
        Task AddAsync(User user);
    }
}
