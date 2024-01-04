using Abp.Domain.Entities;
using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Settings;
using Accounts.DataAccess.UnitOfWork;
using AutoMapper;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class CurrencyService : ICurrencyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CurrencyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<List<CurrencyModel>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var currencies = await _unitOfWork.Currencies.GetAllAsync(paginationSettings, cancellationToken);
            var currenciesList = new List<CurrencyModel>();

            foreach (var currency in currencies)
            {
                CurrencyModel dto = _mapper.Map<CurrencyModel>(currency);
                currenciesList.Add(dto);
            }

            return currenciesList;
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
