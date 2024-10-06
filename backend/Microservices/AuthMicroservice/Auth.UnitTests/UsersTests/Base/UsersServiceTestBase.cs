using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Auth.UnitTests.UsersTests.Base
{
    public abstract class UserServiceTestBase
    {
        protected readonly IFixture _fixture;
        protected readonly Mock<UserManager<AppUser>> _userManagerMock;
        protected readonly Mock<IUnitOfWork> _unitOfWorkMock;
        protected readonly Mock<ICacheRepository> _cacheRepositoryMock;
        protected readonly Mock<ITokenService> _tokenServiceMock;
        protected readonly Mock<IMapper> _mapperMock;
        protected readonly Mock<IMessageProducer> _producerMock;
        protected readonly UsersService _usersService;

        public UserServiceTestBase()
        {
            _fixture = new Fixture();

            _userManagerMock = CreateUserManager();

            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _cacheRepositoryMock = new Mock<ICacheRepository>();
            _tokenServiceMock = new Mock<ITokenService>();
            _mapperMock = new Mock<IMapper>();
            _producerMock = new Mock<IMessageProducer>();

            _usersService = new UsersService(
                _tokenServiceMock.Object,
                _unitOfWorkMock.Object,
                _userManagerMock.Object,
                _mapperMock.Object,
                _producerMock.Object,
                _cacheRepositoryMock.Object
            );
        }

        private Mock<UserManager<AppUser>> CreateUserManager()
        {
            var userStore = new Mock<IUserStore<AppUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;
            options.Setup(o => o.Value).Returns(idOptions);
            var userValidators = new List<IUserValidator<AppUser>>();
            var validator = new Mock<IUserValidator<AppUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<AppUser>>();
            pwdValidators.Add(new PasswordValidator<AppUser>());

            var userManager = new Mock<UserManager<AppUser>>(
                userStore,
                options.Object,
                new PasswordHasher<AppUser>(),
                userValidators,
                pwdValidators,
                new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(),
                null,
                new Mock<ILogger<UserManager<AppUser>>>().Object
            );

            return userManager;
        }

        protected T Create<T>()
        {
            return _fixture.Create<T>();
        }

        protected List<T> CreateMany<T>(int count)
        {
            return _fixture.CreateMany<T>(count).ToList();
        }

        protected void SetupUserManagerWithUser(AppUser user)
        {
            _userManagerMock.Setup(um => um.FindByEmailAsync(user.Email))
                .ReturnsAsync(user);
        }

        protected void SetupMapper<TSource, TDestination>(TSource source, TDestination destination)
        {
            _mapperMock.Setup(m => m.Map<TDestination>(source)).Returns(destination);
        }
    }
}
