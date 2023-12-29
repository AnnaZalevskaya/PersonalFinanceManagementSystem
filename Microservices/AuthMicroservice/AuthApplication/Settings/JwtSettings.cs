namespace Auth.Application.Settings
{
    public class JwtSettings
    {
        public string Expire { get; init; }
        public string Secret { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public string RefreshTokenValidityInDays { get; init;  }
    }
}
