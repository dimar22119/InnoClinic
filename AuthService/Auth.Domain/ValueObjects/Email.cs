using System.Text.RegularExpressions;

namespace Auth.Domain.ValueObjects
{
    public sealed class Email
    {
        public string Value { get; }

        private Email(string value) => Value = value;

        public static Email From(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email is required.", nameof(email));

            var normalized = email.Trim().ToLowerInvariant();

            var regex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!regex.IsMatch(normalized))
                throw new ArgumentException("Email is invalid.", nameof(email));

            return new Email(normalized);
        }

        public override string ToString() => Value;
    }
}
