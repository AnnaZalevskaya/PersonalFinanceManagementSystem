namespace Auth.UnitTests.UsersTests
{
    public class RegistrationTests : UserServiceTestBase
    {
        [Fact]
        public async Task RegisterAsync_WhenUserDoesNotExist_ShouldRegisterUser()
        {
            // Arrange
            var newUserEmail = "newuser@example.com";
            var newUserPassword = "validPassword";

            var registerRequest = _fixture.Build<RegisterRequestModel>()
                .With(r => r.Email, newUserEmail)
                .With(r => r.Password, newUserPassword)
                .Create();

            var newRegisterModel = RegisterResponseModelBuilder.BuildRegisterResponseModel(newUserEmail);

            _unitOfWorkMock.Setup(uw => uw.Users.FindByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AppUser)null);

            _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            _userManagerMock.Setup(um => um.AddToRoleAsync(It.IsAny<AppUser>(), It.IsAny<string>()))
                .ReturnsAsync(IdentityResult.Success);

            var user = _fixture.Build<AppUser>()
                .With(u => u.Email, newUserEmail)
                .Create();

            _mapperMock.Setup(m => m.Map<AppUser>(It.IsAny<RegisterRequestModel>()))
                .Returns(user);
            _mapperMock.Setup(m => m.Map<RegisterResponseModel>(It.IsAny<AppUser>()))
                .Returns(newRegisterModel);

            // Act
            var response = await _usersService.RegisterAsync(registerRequest, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Email.Should().Be(newUserEmail);
        }

        [Fact]
        public async Task RegisterAsync_WhenUserExists_ShouldThrowEntityAlreadyExistsException()
        {
            // Arrange
            var newUserEmail = "john@example.com";
            var newUserPassword = "validPassword";

            var newUser = AppUserBuilder.BuildAppUser(newUserEmail);

            var registerRequest = _fixture.Build<RegisterRequestModel>()
                .With(r => r.Email, newUser.Email)
                .With(r => r.Password, newUserPassword)
                .Create();

            _mapperMock.Setup(m => m.Map<AppUser>(It.IsAny<RegisterRequestModel>()))
                .Returns(newUser);

            _unitOfWorkMock.Setup(uw => uw.Users.FindByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(newUser);

            // Act and Assert
            await Assert.ThrowsAsync<EntityAlreadyExistsException>(() =>
                _usersService.RegisterAsync(registerRequest, CancellationToken.None));

            _unitOfWorkMock.Verify(uw => uw.Users.AddAsync(newUser, CancellationToken.None), Times.Never);
            _userManagerMock.Verify(um => um.AddToRoleAsync(newUser, It.IsAny<string>()), Times.Never);
            _mapperMock.Verify(m => m.Map<RegisterResponseModel>(It.IsAny<AppUser>()), Times.Never);
        }

        [Fact]
        public async Task RegisterAsync_WhenUserCreationFails_ShouldThrowBadCredentialsException()
        {
            // Arrange
            var newUserEmail = "newuser@example.com";
            var newUserPassword = "validPassword";

            var registerRequest = _fixture.Build<RegisterRequestModel>()
                .With(r => r.Email, newUserEmail)
                .With(r => r.Password, newUserPassword)
                .Create();

            _unitOfWorkMock.Setup(uw => uw.Users.FindByEmailAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync((AppUser)null);

            var user = new AppUser { Email = registerRequest.Email };
            _mapperMock.Setup(m => m.Map<AppUser>(It.IsAny<RegisterRequestModel>()))
                .Returns(user);

            _userManagerMock.Setup(um => um.CreateAsync(user, registerRequest.Password))
                .ReturnsAsync(IdentityResult.Failed(new IdentityError { Description = "Error occurred" }));

            // Act and Assert
            var exception = await Assert.ThrowsAsync<BadCredentialsException>(() =>
                _usersService.RegisterAsync(registerRequest, CancellationToken.None));

            _userManagerMock.Verify(um => um.CreateAsync(user, registerRequest.Password), Times.Once);
            _mapperMock.Verify(m => m.Map<RegisterResponseModel>(It.IsAny<AppUser>()), Times.Never);
        }
    }
}
