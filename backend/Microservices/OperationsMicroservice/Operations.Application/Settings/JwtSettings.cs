namespace Operations.Application.Settings
{
    public class JwtSettings
    {
        public string SecretKey { get; init; }
        public string Issuer { get; init; }
        public string Audience { get; init; }
    }
}
