namespace Accounts.BusinessLogic.Settings
{
    public class JwtSettings
    {
        public int Expire { get; init; }
        public string SecretKey { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
        public int RefreshTokenValidityInDays { get; init; }
    }
}
