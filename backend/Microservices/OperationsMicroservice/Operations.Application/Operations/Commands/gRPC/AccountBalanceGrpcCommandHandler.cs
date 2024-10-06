using gRPC.Protos.Server;
using Grpc.Core;
using Operations.Application.Interfaces.gRPC;
using Operations.Application.Models.Consts;

namespace Operations.Application.Operations.Commands.gRPC
{
    public class AccountBalanceGrpcCommandHandler : AccountBalance.AccountBalanceBase
    {
        private readonly IOperationsGrpcRepository _operationsGrpcRepository;

        public AccountBalanceGrpcCommandHandler(IOperationsGrpcRepository operationsGrpcRepository)
        {
            _operationsGrpcRepository = operationsGrpcRepository;
        }

        public async override Task<AccountBalanceResponse> GetAccountBalance(AccountIdRequest request,
            ServerCallContext context)
        {
            var operations = await _operationsGrpcRepository
                .GetByAccountIdAsync(request.AccountId, context.CancellationToken);
            double balance = 0;

            foreach (var operation in operations)
            {
                if (operation.operationCategoryType == OperationCategoryTypeConsts.Income)
                {
                    balance += operation.totalAmount;
                }
                else if (operation.operationCategoryType == OperationCategoryTypeConsts.Expense)
                {
                    balance -= operation.totalAmount;
                }
            }

            var response = new AccountBalanceResponse()
            {
                Balance = balance
            };

            return await Task.FromResult(response);
        }

        public async override Task<ProgressResponse> GetProgress(ProgressRequest request, ServerCallContext context)
        {
            double progress = 0;

            if (request.GoalType == GoalTypeEnum.Reach)
            {
                var req = new AccountIdRequest()
                {
                    AccountId = request.AccountId
                };

                progress = GetAccountBalance(req, context).Result.Balance;
            }
            else
            {
                var operations = await _operationsGrpcRepository
                        .GetByAccountIdByPeriodAsync(request.AccountId, request.StartDate, request.EndDate, 
                        context.CancellationToken);

                foreach (var operation in operations)
                {
                    if (request.GoalType == GoalTypeEnum.Save)
                    {
                        if (operation.operationCategoryType == OperationCategoryTypeConsts.Income)
                        {
                            progress += operation.totalAmount;
                        }
                    }
                    else if (request.GoalType == GoalTypeEnum.Spend)
                    {
                        if (operation.operationCategoryType == OperationCategoryTypeConsts.Expense)
                        {
                            progress += operation.totalAmount;
                        }
                    }
                }
            }

            var response = new ProgressResponse()
            {
                Progress = progress
            };

            return await Task.FromResult(response);
        }
    }
}
