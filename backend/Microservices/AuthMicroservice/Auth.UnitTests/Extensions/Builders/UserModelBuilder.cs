namespace Auth.UnitTests.Extensions.Builders
{
    public static class UserModelBuilder
    {
        public static UserModel BuildUserModel(string email, string role)
        {
            return new UserModel
            {
                Email = email,
                Role = role
            };
        }
    }
}
