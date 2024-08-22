namespace Auth.UnitTests.UsersTests.Base
{
    public abstract class UserServiceTestBase
    {
        protected Mock<ITokenService> _tokenServiceMock;
        protected Mock<IUnitOfWork> _unitOfWorkMock;
        protected Mock<UserManager<AppUser>> _userManagerMock;
        protected Mock<IMapper> _mapperMock;
        protected Mock<IMessageProducer> _producerMock;
        protected Mock<ICacheRepository> _cacheRepositoryMock;
        protected UsersService _usersService;

        public UserServiceTestBase()
        {
            _tokenServiceMock = new Mock<ITokenService>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _mapperMock = new Mock<IMapper>();
            _producerMock = new Mock<IMessageProducer>();
            _cacheRepositoryMock = new Mock<ICacheRepository>();

            var userStore = new Mock<IUserStore<AppUser>>();
            userStore.Setup(x => x.CreateAsync(It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success);
            userStore.Setup(x => x.UpdateAsync(It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success);
            userStore.Setup(x => x.DeleteAsync(It.IsAny<AppUser>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(IdentityResult.Success);
            userStore.Setup(x => x.FindByIdAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AppUser { Id = 1, Email = "john@example.com" });
            userStore.Setup(x => x.FindByNameAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(new AppUser { Id = 1, Email = "john@example.com" });

            _userManagerMock = new Mock<UserManager<AppUser>>(userStore.Object);

            _usersService = new UsersService(
                _tokenServiceMock.Object,
                _unitOfWorkMock.Object,
                _userManagerMock.Object,
                _mapperMock.Object,
                _producerMock.Object,
                _cacheRepositoryMock.Object
            );
        }
    }
}
