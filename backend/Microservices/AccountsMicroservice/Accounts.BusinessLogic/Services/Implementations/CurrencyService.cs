using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Models.Consts;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Exceptions;
using Accounts.DataAccess.Settings;
using Accounts.DataAccess.UnitOfWork;
using AutoMapper;
using Polly;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAsyncPolicy _retryPolicy;

        public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(PollyConsts.MaxRetryAttempts, retryAttempt => TimeSpan.FromSeconds(3));
        }

        public async Task<List<CurrencyModel>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var currencies = await _retryPolicy.ExecuteAsync(() =>
                _unitOfWork.Currencies.GetAllAsync(paginationSettings, cancellationToken));
            var currenciesList = _mapper.Map<List<CurrencyModel>>(currencies);

            return currenciesList;
        }

        public async Task<int> GetRecordsCountAsync()
        {
            return await _unitOfWork.Currencies.GetRecordsCountAsync();
        }

        public async Task<CurrencyModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var currency = await _unitOfWork.Currencies.GetByIdAsync(id, cancellationToken);

            if (currency == null)
            {
                throw new EntityNotFoundException("Currency not found");
            }

            var currencyModel = _mapper.Map<CurrencyModel>(currency);

            return currencyModel;
        }
    }
}
