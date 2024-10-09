using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Models.Consts;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.EFCore.UnitOfWork;
using Accounts.DataAccess.Exceptions;
using Accounts.DataAccess.Settings;
using AutoMapper;
using Polly;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class FinancialGoalTypeService : IFinancialGoalTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IAsyncPolicy _retryPolicy;

        public FinancialGoalTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _retryPolicy = Policy
                .Handle<Exception>()
                .WaitAndRetryAsync(PollyConsts.MaxRetryAttempts, retryAttempt => TimeSpan.FromSeconds(3));
        }

        public async Task<List<FinancialGoalTypeModel>> GetAllAsync(PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var types = await _retryPolicy.ExecuteAsync(() =>
                _unitOfWork.FinancialGoalTypes.GetAllAsync(paginationSettings, cancellationToken));
            var typesList = _mapper.Map<List<FinancialGoalTypeModel>>(types);

            return typesList;
        }

        public async Task<int> GetRecordsCountAsync()
        {
            return await _unitOfWork.FinancialGoalTypes.GetRecordsCountAsync();
        }

        public async Task<FinancialGoalTypeModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var type = await _unitOfWork.FinancialGoalTypes.GetByIdAsync(id, cancellationToken);

            if (type == null)
            {
                throw new EntityNotFoundException();
            }

            var typeModel = _mapper.Map<FinancialGoalTypeModel>(type);

            return typeModel;
        }
    }
}
