namespace Auth.UnitTests.Extensions.Builders
{
    public static class RegisterRequestModelBuilder
    {
        public static RegisterRequestModel BuildRegisterRequestModel(string email, string password)
        {
            return new RegisterRequestModel
            {
                Email = email,
                Password = password
            };
        }
    }
}