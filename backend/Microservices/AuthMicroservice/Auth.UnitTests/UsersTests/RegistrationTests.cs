using Auth.UnitTests.Extensions.Builders;

namespace Auth.UnitTests.UsersTests
{
    public class RegistrationTests : UserServiceTestBase
    {
        [Fact]
        public async Task RegisterAsync_WhenUserDoesNotExist_ShouldRegisterUser()
        {
            // Arrange
            var registerRequest = RegisterRequestModelBuilder.BuildRegisterRequestModel("newuser@example.com", "validPassword");

            _unitOfWorkMock.Setup(uw => uw.Users.FindByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AppUser)null);

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);
            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _mapperMock.Setup(m => m.Map<AppUser>(It.IsAny<RegisterRequestModel>()))
                .Returns(new AppUser { Email = "newuser@example.com" });
            _mapperMock.Setup(m => m.Map<RegisterResponseModel>(It.IsAny<AppUser>()))
                .Returns(new RegisterResponseModel { Email = "newuser@example.com" });

            // Act
            var response = await _usersService.RegisterAsync(registerRequest, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal("newuser@example.com", response.Email);
        }

        [Fact]
        public async Task RegisterAsync_WhenUserExists_ShouldThrowException()
        {
            // Arrange
            var registerRequest = RegisterRequestModelBuilder.BuildRegisterRequestModel("john@example.com", "validPassword");

            _unitOfWorkMock.Setup(uw => uw.Users.FindByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AppUser { Email = "john@example.com" });

            // Act and Assert
            await Assert.ThrowsAsync<NullReferenceException>(() => 
                _usersService.RegisterAsync(registerRequest, CancellationToken.None));
        }

        [Fact]
        public async Task RegisterAsync_WhenUserCreationFails_ShouldThrowException()
        {
            // Arrange
            var registerRequest = RegisterRequestModelBuilder.BuildRegisterRequestModel("newuser@example.com", "validPassword");

            _unitOfWorkMock.Setup(uw => uw.Users.FindByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AppUser)null);

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error occurred" }));

            _mapperMock.Setup(m => m.Map<AppUser>(It.IsAny<RegisterRequestModel>()))
                .Returns(new AppUser { Email = "newuser@example.com" });

            // Act and Assert
            await Assert.ThrowsAsync<BadCredentialsException>(() => _usersService.RegisterAsync(registerRequest, CancellationToken.None));
        }
    }
}
