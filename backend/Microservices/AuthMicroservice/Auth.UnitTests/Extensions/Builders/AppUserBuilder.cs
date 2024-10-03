namespace Auth.UnitTests.Extensions.Builders
{
    public static class AppUserBuilder
    {
        public static AppUser BuildAppUser(string email)
        {
            return new AppUser
            {
                Email = email
            };
        }
    }
}