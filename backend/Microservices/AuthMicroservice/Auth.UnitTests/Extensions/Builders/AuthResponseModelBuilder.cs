namespace Auth.UnitTests.Extensions.Builders
{
    public static class AuthResponseModelBuilder
    {
        public static AuthResponseModel BuildAuthResponseModel(string email)
        {
            return new AuthResponseModel
            {
                Email = email
            };
        }
    }
}
