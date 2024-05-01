using Auth.UnitTests.Extensions.Builders;

namespace Auth.UnitTests.UsersTests
{
    public class UsersTests : UserServiceTestBase
    {
        [Fact]
        public async Task GetAllAsync_WhenCachedUsersExists_ShouldReturnCachedUsers()
        {
            // Arrange
            var paginationSettings = PaginationSettingsBuilder.BuildPaginationSettings(1, 10);
            var cachedUsers = new List<UserModel>
            {
                UserModelBuilder.BuildUserModel("user1@example.com", "Admin"),
                UserModelBuilder.BuildUserModel("user2@example.com", "User")
            };

            _cacheRepositoryMock.Setup(cr => cr.GetCachedLargeDataAsync<UserModel>(paginationSettings))
                .ReturnsAsync(cachedUsers);

            // Act
            var result = await _usersService.GetAllAsync(paginationSettings, CancellationToken.None);

            // Assert
            Assert.Equal(cachedUsers, result);
            _unitOfWorkMock.Verify(uw => uw.Users.GetAllAsync(paginationSettings, CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoCachedUsersExists_ShouldRetrieveUsersFromDatabase()
        {
            // Arrange
            var paginationSettings = PaginationSettingsBuilder.BuildPaginationSettings(1, 10);
            var databaseUsers = new List<AppUser>
            {
                AppUserBuilder.BuildAppUser(1, "user1@example.com"),
                AppUserBuilder.BuildAppUser(2, "user2@example.com")
            };

            _cacheRepositoryMock.Setup(cr => cr.GetCachedLargeDataAsync<UserModel>(paginationSettings))
                .ReturnsAsync(new List<UserModel>());
            _unitOfWorkMock.Setup(uw => uw.Users.GetAllAsync(paginationSettings, CancellationToken.None))
                .ReturnsAsync(databaseUsers);
            _userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(new[] { "Admin", "Client" });
            _mapperMock.Setup(m => m.Map<UserModel>(It.IsAny<AppUser>()))
                .Returns((AppUser u) => new UserModel { Email = u.Email, Role = "Admin" });

            // Act
            var result = await _usersService.GetAllAsync(paginationSettings, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("user1@example.com", result[0].Email);
            Assert.Equal("user2@example.com", result[1].Email);
            _cacheRepositoryMock.Verify(cr => 
                cr.CacheLargeDataAsync(paginationSettings, It.IsAny<List<UserModel>>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WhenRetrievingUsersFromDatabase_ShouldMapToUserModel()
        {
            // Arrange
            var paginationSettings = PaginationSettingsBuilder.BuildPaginationSettings(1, 10);
            var databaseUsers = new List<AppUser>
            {
                AppUserBuilder.BuildAppUser(1, "user1@example.com"),
                AppUserBuilder.BuildAppUser(2, "user2@example.com")
            };

            _cacheRepositoryMock.Setup(cr => cr.GetCachedLargeDataAsync<UserModel>(paginationSettings))
                .ReturnsAsync(new List<UserModel>());
            _unitOfWorkMock.Setup(uw => uw.Users.GetAllAsync(paginationSettings, CancellationToken.None))
                .ReturnsAsync(databaseUsers);
            _userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<AppUser>()))
                .ReturnsAsync(new[] { "Admin", "Client" });
            _mapperMock.Setup(m => m.Map<UserModel>(It.IsAny<AppUser>()))
                .Returns((AppUser u) => new UserModel { Email = u.Email, Role = "Admin" });

            // Act
            var result = await _usersService.GetAllAsync(paginationSettings, CancellationToken.None);

            // Assert
            Assert.Equal(2, result.Count);
            Assert.Equal("user1@example.com", result[0].Email);
            Assert.Equal("Admin", result[0].Role);
            Assert.Equal("user2@example.com", result[1].Email);
            Assert.Equal("Admin", result[1].Role);
        }
    }
}
