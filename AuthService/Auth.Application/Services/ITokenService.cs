using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Services
{
    public interface ITokenService
    {
        string GenerateToken(Guid userId, string email, string role);
    }
}
