using System;
using System.Collections.Generic;
using System.Text;

namespace Auth.Domain.ValueObjects
{
    public sealed class PasswordHash
    {
        public string Value { get; }

        public PasswordHash(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new ArgumentException("Password hash is empty");

            Value = value;
        }
    }
}
