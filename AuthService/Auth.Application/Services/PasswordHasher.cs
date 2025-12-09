using System.Security.Cryptography;

namespace Auth.Application.Services
{
    public class PasswordHasher : IPasswordHasher
    {
        private const int SaltSize = 16; // 128 bit
        private const int KeySize = 32;  // 256 bit
        private const int Iterations = 100_000;
        private const char Delimiter = '.';
        public string Hash(string plain)
        {
            if (plain is null) throw new ArgumentNullException(nameof(plain));

            using var rng = RandomNumberGenerator.Create();
            var salt = new byte[SaltSize];
            rng.GetBytes(salt);

            using var pbkdf2 = new Rfc2898DeriveBytes(plain, salt, Iterations, HashAlgorithmName.SHA256);
            var key = pbkdf2.GetBytes(KeySize);

            var parts = new[]
            {
                Iterations.ToString(),
                Convert.ToBase64String(salt),
                Convert.ToBase64String(key)
            };

            return string.Join(Delimiter, parts);
        }

        public bool Verify(string hashed, string plain)
        {
            if (hashed is null) throw new ArgumentNullException(nameof(hashed));
            if (plain is null) throw new ArgumentNullException(nameof(plain));

            var parts = hashed.Split(Delimiter);
            if (parts.Length != 3) return false;

            if (!int.TryParse(parts[0], out var iterations)) return false;

            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            using var pbkdf2 = new Rfc2898DeriveBytes(plain, salt, iterations, HashAlgorithmName.SHA256);
            var keyToCheck = pbkdf2.GetBytes(key.Length);

            return CryptographicOperations.FixedTimeEquals(keyToCheck, key);
        }
    }
}
