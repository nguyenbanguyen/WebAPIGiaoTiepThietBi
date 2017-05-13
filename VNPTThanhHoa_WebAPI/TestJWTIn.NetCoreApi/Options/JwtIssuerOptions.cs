using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;

namespace TestJWTIn.NetCoreApi.Options
{
    public class JwtIssuerOptions
    {
        public string Issuer { get; set; }
        public string Subject { get; set; }
        public string Audience { get; set; }
        public DateTime NotBefore { get; set; } = DateTime.UtcNow;
        public DateTime IssuedAt { get; set; } = DateTime.UtcNow;
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(5);
        public DateTime Expiration => IssuedAt.Add(ValidFor);
        public Func<Task<string>> JtiGeneration => () => Task.FromResult(Guid.NewGuid().ToString());
        public SigningCredentials SigningCredentials { get; set; }
    }
}
