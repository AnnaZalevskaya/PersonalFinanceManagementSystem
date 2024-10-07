using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Dapper.Repositories.Interfaces;
using AutoMapper;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class AccountStatisticsService : IAccountStatisticsService
    {
        IAccountStatisticsRepository _repository;
        private readonly IMapper _mapper;

        public AccountStatisticsService(IAccountStatisticsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<List<AccountStatisticsModel>> GetStatisticByAccountsAsync(int accountTypeParam)
        {
            var statistic = await _repository.GetStatisticByAccountsAsync(accountTypeParam);
            var models = _mapper.Map<List<AccountStatisticsModel>>(statistic);

            return models;
        }
    }
}
