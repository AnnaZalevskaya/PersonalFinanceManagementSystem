using Abp.Domain.Entities;
using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Settings;
using Accounts.DataAccess.UnitOfWork;
using AutoMapper;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class FinancialAccountService : IFinancialAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public FinancialAccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task AddAsync(FinancialAccountModel addModel, CancellationToken cancellationToken)
        {
            var account = _mapper.Map<FinancialAccount>(addModel);
            await _unitOfWork.FinancialAccounts.AddAsync(account, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id, CancellationToken cancellationToken)
        {
            await _unitOfWork.FinancialAccounts.DeleteAsync(id, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<List<FinancialAccountModel>> GetAllAsync(PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            var accounts = await _unitOfWork.FinancialAccounts.GetAllAsync(paginationSettings, cancellationToken);
            var accountsList = new List<FinancialAccountModel>();

            foreach (var account in accounts)
            {
                FinancialAccountModel dto = _mapper.Map<FinancialAccountModel>(account);
                accountsList.Add(dto);
            }

            return accountsList;
        }

        public async Task<List<FinancialAccountModel>> GetAccountsByUserIdAsync(int userId, 
            PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            var accounts = await _unitOfWork.FinancialAccounts
                .GetAccountsByUserIdAsync(userId, paginationSettings, cancellationToken);
            var accountsList = new List<FinancialAccountModel>();

            foreach (var account in accounts)
            {
                FinancialAccountModel dto = _mapper.Map<FinancialAccountModel>(account);
                accountsList.Add(dto);
            }

            return accountsList;
        }

        public async Task<FinancialAccountModel> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.FinancialAccounts.GetByIdAsync(id, cancellationToken);

            if (account == null)
            {
                throw new EntityNotFoundException("Account not found");
            }

            var accountModel = _mapper.Map<FinancialAccountModel>(account);

            return accountModel;
        }

        public async Task UpdateAsync(int id, FinancialAccountModel updateModel, CancellationToken cancellationToken)
        {
            var account = _mapper.Map<FinancialAccount>(updateModel);

            if (account == null)
            {
                throw new EntityNotFoundException("Account not found");
            }

            await _unitOfWork.FinancialAccounts.UpdateAsync(id, account, cancellationToken);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}
