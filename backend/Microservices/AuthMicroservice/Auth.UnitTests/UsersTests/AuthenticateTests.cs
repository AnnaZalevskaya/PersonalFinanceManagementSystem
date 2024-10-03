namespace Auth.UnitTests.UsersTests
{
    public class AuthenticateTests : UserServiceTestBase
    {
        [Fact]
        public async Task AuthenticateAsync_WhenUserIsNotFound_ShouldThrowEntityNotFoundException()
        {
            // Arrange
            var authRequest = Create<AuthRequestModel>();
            authRequest = AuthRequestModelBuilder.BuildAuthRequestModel("john@example.com", "password");

            _userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>()))
                .ReturnsAsync((AppUser)null);

            // Act and Assert
            await Assert.ThrowsAsync<EntityNotFoundException>(() =>
                _usersService.AuthenticateAsync(authRequest, CancellationToken.None));
        }

        [Fact]
        public async Task AuthenticateAsync_WhenPasswordIsInvalid_ShouldThrowArgumentException()
        {
            // Arrange
            var authRequest = Create<AuthRequestModel>();
            authRequest = AuthRequestModelBuilder.BuildAuthRequestModel("john@example.com", "validPassword");

            var user = Create<AppUser>();
            user = AppUserBuilder.BuildAppUser("john@example.com");

            SetupUserManagerWithUser(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(false);

            // Act and Assert
            await Assert.ThrowsAsync<BadCredentialsException>(() =>
                _usersService.AuthenticateAsync(authRequest, CancellationToken.None));
        }

        [Fact]
        public async Task AuthenticateAsync_WhenUserIsFound_ShouldReturnAuthResponseModel()
        {
            // Arrange
            var authRequest = Create<AuthRequestModel>();
            authRequest = AuthRequestModelBuilder.BuildAuthRequestModel("john@example.com", "validPassword");

            var user = Create<AppUser>();
            user = AppUserBuilder.BuildAppUser("john@example.com");
            var roles = new List<string> { "Admin" };
            var token = "accessToken";

            SetupUserManagerWithUser(user);
            _userManagerMock.Setup(um => um.CheckPasswordAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(true);
            _userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(roles);

            _unitOfWorkMock.Setup(uw => uw.UserRoles.GetRoleIdsAsync(It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<long>());
            _unitOfWorkMock.Setup(uw => uw.Roles.GetRoleIdsAsync(It.IsAny<IEnumerable<long>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new List<IdentityRole<long>>());

            _tokenServiceMock.Setup(ts => ts.GetToken(It.IsAny<AppUser>(), It.IsAny<List<IdentityRole<long>>>()))
                .Returns(token);

            _mapperMock.Setup(m => m.Map<AuthResponseModel>(It.IsAny<AppUser>()))
                .Returns(AuthResponseModelBuilder.BuildAuthResponseModel("john@example.com"));

            // Act
            var response = await _usersService.AuthenticateAsync(authRequest, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Email.Should().Be(user.Email);
            response.Role.Should().Be(roles[0]);
            response.Token.Should().Be(token);
        }
    }
}