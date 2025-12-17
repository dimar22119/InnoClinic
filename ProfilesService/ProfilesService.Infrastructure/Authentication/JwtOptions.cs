using System;
using System.Collections.Generic;
using System.Text;

namespace ProfilesService.Infrastructure.Authentication
{
    public class JwtOptions
    {
        public string Issuer { get; set; } = string.Empty;
        public string Audience { get; set; } = string.Empty;
        public string Secret { get; set; } = string.Empty;
        public int ExpiresMinutes { get; set; } = 60;
    }
}
