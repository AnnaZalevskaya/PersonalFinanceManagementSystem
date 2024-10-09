using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Exceptions;
using Accounts.DataAccess.Settings;
using AutoMapper;
using gRPC.Protos.Client;
using static gRPC.Protos.Client.AccountBalance;
using GoalTypeEnum = gRPC.Protos.Client.GoalTypeEnum;
using AccountsEFCoreUnitOfWork = Accounts.DataAccess.EFCore.UnitOfWork.IUnitOfWork;
using AccountsDapperUnitOfWork = Accounts.DataAccess.Dapper.UnitOfWork.IUnitOfWork;
using Accounts.BusinessLogic.Models.Enums;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class FinancialGoalService : IFinancialGoalService
    {
        private readonly AccountsEFCoreUnitOfWork _efUnitOfWork;
        private readonly AccountsDapperUnitOfWork _dapperUnitOfWork;
        private readonly IMapper _mapper;
        private readonly AccountBalanceClient _balanceClient;

        public FinancialGoalService(AccountsEFCoreUnitOfWork efUnitOfWork, AccountsDapperUnitOfWork dapperUnitOfWork, 
            IMapper mapper, AccountBalanceClient balanceClient)
        {
            _efUnitOfWork = efUnitOfWork;
            _dapperUnitOfWork = dapperUnitOfWork;
            _mapper = mapper;
            _balanceClient = balanceClient;
        }

        public async Task<List<FinancialGoalModel>> GetFinancialGoalsAsync(PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            
            var goals = await _efUnitOfWork.FinancialGoals.GetAllAsync(paginationSettings, cancellationToken);
            var goalsList = _mapper.Map<List<FinancialGoalModel>>(goals);

            return goalsList;
        }

        public async Task<List<FinancialGoalModel>> GetAccountFinancialGoalsAsync(int accountId, PaginationSettings paginationSettings,
            CancellationToken cancellationToken)
        {
            var goals = await _efUnitOfWork.FinancialGoals.GetAccountGoalsAsync(accountId, paginationSettings, 
                cancellationToken);
            var goalsList = _mapper.Map<List<FinancialGoalModel>>(goals);

            foreach( var goal in goalsList)
            {
                var request = new ProgressRequest()
                {
                    AccountId = goal.AccountId,
                    GoalType = (GoalTypeEnum)goal.TypeId,
                    StartDate = goal.StartDate.Date.ToString(),
                    EndDate = goal.EndDate.Date.ToString()
                };

                var progress = _balanceClient.GetProgress(request, cancellationToken: cancellationToken);
                goal.Progress = progress.Progress;
            }

            return goalsList;
        }

        public async Task<int> GetAccountRecordsCountAsync(int accountId)
        {
            return await _efUnitOfWork.FinancialGoals.GetAccountRecordsCountAsync(accountId);
        }

        public async Task<FinancialGoalModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var goal = await _efUnitOfWork.FinancialGoals.GetByIdAsync(id, cancellationToken);

            if (goal == null)
            {
                throw new EntityNotFoundException();
            }

            var goalModel = _mapper.Map<FinancialGoalModel>(goal);

            var request = new ProgressRequest()
            {
                AccountId = goal.AccountId,
                GoalType = (GoalTypeEnum)goalModel.TypeId,
                StartDate = goalModel.StartDate.Date.ToString(),
                EndDate = goalModel.EndDate.Date.ToString()
            };

            var progress = await _balanceClient.GetProgressAsync(request, cancellationToken: cancellationToken);
            goalModel.Progress = progress.Progress;

            return goalModel;
        }

        public async Task CreateFinancialGoalAsync(FinancialGoalActionModel financialGoal, CancellationToken cancellationToken)
        {
            var goal = _mapper.Map<FinancialGoal>(financialGoal);
            await _efUnitOfWork.FinancialGoals.AddAsync(goal, cancellationToken);

            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateAsync(int id, FinancialGoalActionModel updateModel,
            CancellationToken cancellationToken)
        {
            var goal = await _efUnitOfWork.FinancialGoals.GetByIdAsync(id, cancellationToken);

            if (goal == null)
            {
                throw new EntityNotFoundException();
            }

            var updateGoal = _mapper.Map<FinancialGoal>(updateModel);
            updateGoal.Id = goal.Id;

            await _efUnitOfWork.FinancialGoals.UpdateAsync(id, updateGoal, cancellationToken);

            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            var account = await _efUnitOfWork.FinancialGoals.GetByIdAsync(id, cancellationToken);

            if (account == null)
            {
                throw new EntityNotFoundException();
            }

            await _efUnitOfWork.FinancialGoals.DeleteAsync(id, cancellationToken);

            await _efUnitOfWork.SaveChangesAsync(cancellationToken);
        }

        public async Task UpdateGoalStatusAsync(int goalId, GoalStatusEnum newStatus)
        {
            await _dapperUnitOfWork.GoalStatuses.UpdateGoalStatusAsync(goalId, (int)newStatus);
        }
    }
}
