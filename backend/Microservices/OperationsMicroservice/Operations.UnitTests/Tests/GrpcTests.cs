using gRPC.Protos.Server;
using Grpc.Core;
using Moq;
using Operations.Application.Interfaces.gRPC;
using Operations.Application.Models.Consts;
using Operations.Application.Operations.Commands.gRPC;
using Operations.Core.Entities;

namespace Operations.UnitTests.Tests
{
    [TestFixture]
    public class GrpcTests
    {
        private AccountBalanceGrpcCommandHandler _handler;
        private Mock<IOperationsGrpcRepository> _repositoryMock;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _repositoryMock = _fixture.Freeze<Mock<IOperationsGrpcRepository>>();
            _handler = new AccountBalanceGrpcCommandHandler(_repositoryMock.Object);
        }

        [Test]
        public void GetAccountBalance_ShouldReturnCorrectBalance()
        {
            // Arrange
            var accountId = _fixture.Create<int>();

            var operations = _fixture.Build<AggregationResult>()
                .With(o => o.totalAmount, -100.0) 
                .With(o => o.operationCategoryType, OperationCategoryTypeConsts.Expense)
                .CreateMany(5) 
                .ToList();

            _repositoryMock
                .Setup(x => x.GetByAccountIdAsync(accountId, CancellationToken.None))
                .ReturnsAsync(operations.AsEnumerable());

            var request = _fixture.Build<AccountIdRequest>()
                .With(r => r.AccountId, accountId)
                .Create();

            var serverCallContext = new Mock<ServerCallContext>();

            // Act
            var response = _handler.GetAccountBalance(request, serverCallContext.Object).Result;

            // Assert
            var expectedBalance = operations.Sum(x =>
                x.operationCategoryType == OperationCategoryTypeConsts.Income ? x.totalAmount : -x.totalAmount);

            response.Balance.Should().Be(expectedBalance);
        }
    }
}