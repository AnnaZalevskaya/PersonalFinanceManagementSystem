namespace Auth.UnitTests.UsersTests
{
    public class RegistrationTests : UserServiceTestBase
    {
        [Fact]
        public async Task RegisterAsync_WhenUserDoesNotExist_ShouldRegisterUser()
        {
            // Arrange
            var registerRequest = _fixture.Build<RegisterRequestModel>()
                .With(r => r.Email, "newuser@example.com")
                .With(r => r.Password, "validPassword")
                .Create();

            _unitOfWorkMock.Setup(uw => uw.Users.FindByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AppUser)null);

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var user = _fixture.Build<AppUser>()
                .With(u => u.Email, "newuser@example.com")
                .Create();

            _mapperMock.Setup(m => m.Map<AppUser>(It.IsAny<RegisterRequestModel>()))
                .Returns(user);
            _mapperMock.Setup(m => m.Map<RegisterResponseModel>(It.IsAny<AppUser>()))
                .Returns(new RegisterResponseModel { Email = "newuser@example.com" });

            // Act
            var response = await _usersService.RegisterAsync(registerRequest, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Email.Should().Be("newuser@example.com");
        }

        [Fact]
        public async Task RegisterAsync_WhenUserExists_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var registerRequest = _fixture.Build<RegisterRequestModel>()
                .With(r => r.Email, "john@example.com")
                .With(r => r.Password, "validPassword")
                .Create();

            _mapperMock.Setup(m => m.Map<AppUser>(It.IsAny<RegisterRequestModel>()))
                .Returns(new AppUser { Email = "john@example.com" });

            _unitOfWorkMock.Setup(uw => uw.Users.FindByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AppUser { Email = "john@example.com" });

            // Act and Assert
            await Assert.ThrowsAsync<EntityAlreadyExistsException>(() =>
                _usersService.RegisterAsync(registerRequest, CancellationToken.None));
        }

        [Fact]
        public async Task RegisterAsync_WhenUserCreationFails_ShouldThrowBadCredentialsException()
        {
            // Arrange
            var registerRequest = _fixture.Build<RegisterRequestModel>()
                .With(r => r.Email, "newuser@example.com")
                .With(r => r.Password, "validPassword")
                .Create();

            _unitOfWorkMock.Setup(uw => uw.Users.FindByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AppUser)null);

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error occurred" }));

            var user = _fixture.Build<AppUser>()
                .With(u => u.Email, "newuser@example.com")
                .Create();

            _mapperMock.Setup(m => m.Map<AppUser>(It.IsAny<RegisterRequestModel>()))
                .Returns(user);

            // Act and Assert
            await Assert.ThrowsAsync<BadCredentialsException>(() =>
                _usersService.RegisterAsync(registerRequest, CancellationToken.None));
        }
    }
}
