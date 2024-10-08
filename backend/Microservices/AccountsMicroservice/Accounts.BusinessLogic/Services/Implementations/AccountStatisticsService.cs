using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Dapper.UnitOfWork;
using AutoMapper;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class AccountStatisticsService : IAccountStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AccountStatisticsService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<AccountStatisticsModel>> GetStatisticByAccountsAsync(int accountTypeParam)
        {
            var statistic = await _unitOfWork.FinancialAccountStatistics.GetStatisticByAccountsAsync(accountTypeParam);
            var models = _mapper.Map<List<AccountStatisticsModel>>(statistic);

            return models;
        }
    }
}
