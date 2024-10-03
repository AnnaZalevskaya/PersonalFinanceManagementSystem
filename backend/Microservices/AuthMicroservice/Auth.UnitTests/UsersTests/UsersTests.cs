using Auth.Application.Settings;

namespace Auth.UnitTests.UsersTests
{
    public class UsersTests : UserServiceTestBase
    {
        [Fact]
        public async Task GetAllAsync_WhenCachedUsersExist_ShouldReturnCachedUsers()
        {
            // Arrange
            var paginationSettings = Create<PaginationSettings>();
            var cachedUsers = CreateMany<UserModel>(2).ToList();

            _cacheRepositoryMock.Setup(cr => cr.GetCachedLargeDataAsync<UserModel>(paginationSettings))
                .ReturnsAsync(cachedUsers);

            // Act
            var result = await _usersService.GetAllAsync(paginationSettings, CancellationToken.None);

            // Assert
            result.Should().BeEquivalentTo(cachedUsers);
            _unitOfWorkMock.Verify(uw => uw.Users.GetAllAsync(paginationSettings, CancellationToken.None), Times.Never);
        }

        [Fact]
        public async Task GetAllAsync_WhenNoCachedUsersExists_ShouldRetrieveUsersFromDatabase()
        {
            // Arrange
            var paginationSettings = _fixture.Build<PaginationSettings>()
                .With(p => p.PageNumber, 1)
                .With(p => p.PageSize, 10)
                .Create();

            var user1 = _fixture.Build<AppUser>()
                .With(u => u.Email, "user1@example.com")
                .Create();

            var user2 = _fixture.Build<AppUser>()
                .With(u => u.Email, "user2@example.com")
                .Create();

            var databaseUsers = new List<AppUser> { user1, user2 };

            var adminRole = "Admin";
            var clientRole = "Client";

            _cacheRepositoryMock.Setup(cr => cr.GetCachedLargeDataAsync<UserModel>(paginationSettings))
                .ReturnsAsync([]);

            _unitOfWorkMock.Setup(uw => uw.Users.GetAllAsync(paginationSettings, CancellationToken.None))
                .ReturnsAsync(databaseUsers);

            _userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<AppUser>()))
                .ReturnsAsync([adminRole, clientRole]);

            _mapperMock.Setup(m => m.Map<UserModel>(It.IsAny<AppUser>()))
                .Returns((AppUser u) => new UserModel { Email = u.Email, Role = adminRole });

            // Act
            var result = await _usersService.GetAllAsync(paginationSettings, CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result[0].Email.Should().Be(user1.Email);
            result[1].Email.Should().Be(user2.Email);

            _cacheRepositoryMock.Verify(cr =>
                cr.CacheLargeDataAsync(paginationSettings, It.IsAny<List<UserModel>>()), Times.Once);
        }

        [Fact]
        public async Task GetAllAsync_WhenRetrievingUsersFromDatabase_ShouldMapToUserModel()
        {
            // Arrange
            var paginationSettings = _fixture.Build<PaginationSettings>()
                .With(p => p.PageNumber, 1)
                .With(p => p.PageSize, 10)
                .Create();

            var user1 = _fixture.Build<AppUser>()
                .With(u => u.Email, "user1@example.com")
                .Create(); 

            var user2 = _fixture.Build<AppUser>()
                .With(u => u.Email, "user2@example.com")
                .Create();

            var databaseUsers = new List<AppUser> { user1, user2 };

            var adminRole = "Admin";
            var clientRole = "Client";

            _cacheRepositoryMock.Setup(cr => cr.GetCachedLargeDataAsync<UserModel>(paginationSettings))
                .ReturnsAsync([]);
            _unitOfWorkMock.Setup(uw => uw.Users.GetAllAsync(paginationSettings, CancellationToken.None))
                .ReturnsAsync(databaseUsers);
            _userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<AppUser>()))
                .ReturnsAsync([adminRole, clientRole]);
            _mapperMock.Setup(m => m.Map<UserModel>(It.IsAny<AppUser>()))
                .Returns((AppUser u) => new UserModel { Email = u.Email, Role = adminRole });

            // Act
            var result = await _usersService.GetAllAsync(paginationSettings, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result[0].Email.Should().Be(user1.Email);
            result[0].Role.Should().Be(adminRole);
            result[1].Email.Should().Be(user2.Email);
            result[1].Role.Should().Be(adminRole);
        }
    }
}
