namespace Accounts.DataAccess.Settings
{
    public class CacheSettings
    {
        public TimeSpan AbsoluteExpirationRelativeToNow { get; init; }
        public TimeSpan SlidingExpiration { get; init; }
    }
}
