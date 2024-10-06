namespace Auth.UnitTests.Extensions.Builders
{
    public static class RegisterResponseModelBuilder
    {
        public static RegisterResponseModel BuildRegisterResponseModel(string email)
        {
            return new RegisterResponseModel
            {
                Email = email
            };
        }
    }
}
