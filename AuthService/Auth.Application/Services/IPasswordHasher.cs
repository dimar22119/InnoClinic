using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Application.Services
{
    public interface IPasswordHasher
    {
        string Hash(string plain);
        bool Verify(string hashed, string plain);
    }
}
