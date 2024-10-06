namespace Accounts.DataAccess.Settings
{
    public class CacheSettings
    {
        public string KeyPrefix { get; init; }
        public TimeSpan AbsoluteExpirationRelativeToNow { get; init; }
        public TimeSpan SlidingExpiration { get; init; }
    }
}
