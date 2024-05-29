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
    public class FinancialAccountTypeService : IFinancialAccountTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAsyncPolicy _retryPolicy;

        public FinancialAccountTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(PollyConsts.MaxRetryAttempts, retryAttempt => TimeSpan.FromSeconds(3));
        }

        public async Task<List<FinancialAccountTypeModel>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var types = await _retryPolicy.ExecuteAsync(() => 
                _unitOfWork.FinancialAccountTypes.GetAllAsync(paginationSettings, cancellationToken));
            var typesList = _mapper.Map<List<FinancialAccountTypeModel>>(types);

            return typesList;
        }

        public async Task<FinancialAccountTypeModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var type = await _unitOfWork.FinancialAccountTypes.GetByIdAsync(id, cancellationToken);

            if (type == null)
            {
                throw new EntityNotFoundException();
            }

            var typeModel = _mapper.Map<FinancialAccountTypeModel>(type);

            return typeModel;
        }
    }
}
