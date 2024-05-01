namespace Auth.UnitTests.Extensions.Builders
{
    public static class AppUserBuilder
    {
        public static AppUser BuildAppUser(int id, string email)
        {
            return new AppUser
            {
                Id = id,
                Email = email
            };
        }
    }
}