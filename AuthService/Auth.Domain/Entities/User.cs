using Auth.Domain.ValueObjects;

namespace Auth.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public Email Email { get; private set; }
        public PasswordHash PasswordHash { get; private set; }
        public DateTime CreatedAt { get; set; }
        public Role Role { get; private set; }

        private User() { }

        public User(Email email, PasswordHash passwordHash, Role role = Role.User)
        {
            Id = Guid.NewGuid();
            Email = email;
            PasswordHash = passwordHash;
            CreatedAt = DateTime.UtcNow;
            Role = role;
        }

        public void AssignRole(Role role)
        {
            Role = role;
        }
    }
}
