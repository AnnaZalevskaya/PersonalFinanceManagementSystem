using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auth.Application.Settings
{
    public class JwtSettings
    {
        public string Expire { get; }
        public string Secret { get; }
        public string Issuer { get; }
        public string Audience { get; }
        public string TokenValidityInMinutes { get; }
        public string RefreshTokenValidityInDays { get; }
    }
}
