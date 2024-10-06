namespace Auth.UnitTests.Extensions.Builders
{
    public static class AuthRequestModelBuilder
    {
        public static AuthRequestModel BuildAuthRequestModel(string email, string password)
        {
            return new AuthRequestModel
            {
                Email = email,
                Password = password
            };
        }
    }
}