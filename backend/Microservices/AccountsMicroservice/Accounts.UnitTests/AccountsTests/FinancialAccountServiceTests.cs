using Accounts.BusinessLogic.Consumers;
using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Producers;
using Accounts.BusinessLogic.Services.Implementations;
using Accounts.DataAccess.Repositories.Interfaces;
using Accounts.DataAccess.Settings;
using Accounts.DataAccess.UnitOfWork;
using AutoMapper;
using static gRPC.Protos.Client.AccountBalance;

namespace Accounts.UnitTests.AccountsTests
{
    [TestFixture]
    public class FinancialAccountServiceTests
    {
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private Mock<IMapper> _mockMapper;
        private Mock<IMessageConsumer> _mockConsumer;
        private Mock<IMessageProducer> _mockProducer;
        private Mock<AccountBalanceClient> _mockBalanceClient;
        private Mock<ICacheRepository> _mockCacheRepository;
        private FinancialAccountService _service;

        [SetUp]
        public void Setup()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _mockConsumer = new Mock<IMessageConsumer>();
            _mockProducer = new Mock<IMessageProducer>();
            _mockBalanceClient = new Mock<AccountBalanceClient>();
            _mockCacheRepository = new Mock<ICacheRepository>();

            _service = new FinancialAccountService(
                _mockUnitOfWork.Object,
                _mockMapper.Object,
                _mockConsumer.Object,
                _mockProducer.Object,
                _mockBalanceClient.Object,
                _mockCacheRepository.Object);
        }

        [Test]
        public async Task GetAllAsync_ShouldReturnCachedAccounts_WhenCacheIsNotEmpty()
        {
            // Arrange
            var paginationSettings = new PaginationSettings();
            var cachedAccounts = new List<FinancialAccountModel> { new FinancialAccountModel() };

            _mockCacheRepository.Setup(c => c.GetCachedLargeDataAsync<FinancialAccountModel>(paginationSettings, ""))
                .ReturnsAsync(cachedAccounts);

            // Act
            var result = await _service.GetAllAsync(paginationSettings, CancellationToken.None);

            // Assert
            Assert.AreEqual(cachedAccounts.Count, result.Count);
        }

        [Test]
        public async Task GetByIdAsync_ShouldReturnCachedAccount_WhenCacheIsNotEmpty()
        {
            // Arrange
            var accountId = 1;
            var cachedAccount = new FinancialAccountModel();

            _mockCacheRepository.Setup(c => c.GetCachedDataAsync<FinancialAccountModel>(accountId))
                .ReturnsAsync(cachedAccount);

            // Act
            var result = await _service.GetByIdAsync(accountId, CancellationToken.None);

            // Assert
            Assert.AreEqual(cachedAccount, result);
        }
    }
}