namespace Auth.Application.Settings
{
    public class JwtSettings
    {
        public int ExpirationTime { get; init; }
        public int RefreshTokenValidityInDays { get; init; }
        public string SecretKey { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }    
    }
}
