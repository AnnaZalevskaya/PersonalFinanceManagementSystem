using Abp.Domain.Entities;
using Accounts.BusinessLogic.Consumers;
using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Producers;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.DataAccess.Entities;
using Accounts.DataAccess.Repositories.Interfaces;
using Accounts.DataAccess.Settings;
using Accounts.DataAccess.UnitOfWork;
using AutoMapper;
using gRPC.Protos.Client;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class FinancialAccountService : IFinancialAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageConsumer _consumer;
        private readonly IMessageProducer _producer;
        private readonly AccountBalance.AccountBalanceClient _balanceClient;
        private readonly ICacheRepository _cacheRepository;
        
        public FinancialAccountService(IUnitOfWork unitOfWork, IMapper mapper,
            IMessageConsumer consumer, IMessageProducer producer,
            AccountBalance.AccountBalanceClient balanceClient, ICacheRepository cacheRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _consumer = consumer;
            _producer = producer;
            _balanceClient = balanceClient;
            _cacheRepository = cacheRepository;
        }

        public async Task AddAsync(FinancialAccountActionModel addModel, CancellationToken cancellationToken)
        {
            int userId = _consumer.ConsumeMessage(addModel.UserId);

            if (userId == 0)
            {
                throw new Exception("The user is not logged in");
            }

            var account = _mapper.Map<FinancialAccount>(addModel);
            await _unitOfWork.FinancialAccounts.AddAsync(account, cancellationToken);
           
            await _unitOfWork.SaveChangesAsync();

            await _cacheRepository.SetDataCacheAsync(account.Id, account);
        }

        public async Task DeleteAsync(int userId, int id, CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.FinancialAccounts.GetByIdAsync(id, cancellationToken);

            if (account == null)
            {
                throw new EntityNotFoundException("Account not found");
            }

            int receivedUserId = _consumer.ConsumeMessage(account.UserId);

            if (receivedUserId == 0 || userId != receivedUserId)
            {
                throw new Exception("The user is not logged in");
            }

            await _unitOfWork.FinancialAccounts.DeleteAsync(id, cancellationToken);

            await _unitOfWork.SaveChangesAsync();

            await _cacheRepository.RemoveDataCacheAsync(id);
        }

        public async Task<List<FinancialAccountModel>> GetAllAsync(PaginationSettings paginationSettings, 
            CancellationToken cancellationToken)
        {
            var accounts = await _unitOfWork.FinancialAccounts.GetAllAsync(paginationSettings, cancellationToken);  
            var accountsList = _mapper.Map<List<FinancialAccountModel>>(accounts);

            var cachedData = new List<FinancialAccountModel>();

            foreach (var account in accountsList)
            {
                cachedData.Add(await _cacheRepository.GetDataCacheAsync<FinancialAccountModel>(account.Id));
            }

            if (cachedData.Count == 0)
            {
                throw new Exception("There is no relevant information in the cache");
            }

            foreach (var account in accountsList)
            {
                var request = new AccountIdRequest
                {
                    AccountId = account.Id
                };

                var accountBalanceResponse = await _balanceClient
                    .GetAccountBalanceAsync(request, cancellationToken: cancellationToken);

                if (accountBalanceResponse == null)
                {
                    throw new Exception("Response is null");
                }

                account.Balance = accountBalanceResponse.Balance;
            }

            return accountsList;
        }

        public async Task<List<FinancialAccountModel>> GetAccountsByUserIdAsync(int userId,
            PaginationSettings paginationSettings, CancellationToken cancellationToken)
        {
            int id = _consumer.ConsumeMessage(userId);

            if (id == 0)
            {
                throw new Exception("The user is not logged in");
            }

            var accounts = await _unitOfWork.FinancialAccounts
                .GetAccountsByUserIdAsync(id, paginationSettings, cancellationToken);
            var accountsList = _mapper.Map<List<FinancialAccountModel>>(accounts);
            var cachedData = new List<FinancialAccountModel>();

            foreach (var account in accountsList)
            {
                cachedData.Add(await _cacheRepository.GetDataCacheAsync<FinancialAccountModel>(account.Id));
            }

            if (cachedData.Count == 0)
            {
                throw new Exception("There is no relevant information in the cache");
            }

            foreach (var account in accountsList)
            {
                var request = new AccountIdRequest
                {
                    AccountId = account.Id
                };

                var accountBalanceResponse = await _balanceClient
                    .GetAccountBalanceAsync(request, cancellationToken: cancellationToken);

                if (accountBalanceResponse == null)
                {
                    throw new Exception("Response is null");
                }

                account.Balance = accountBalanceResponse.Balance;
            }

            _producer.SendMessages(accountsList);   

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

            var cachedObj = await _cacheRepository.GetDataCacheAsync<FinancialAccountModel>(id);

            if (cachedObj == null)
            {
                throw new Exception("There is no relevant object in the cache");
            }

            var request = new AccountIdRequest()
            {
                AccountId = account.Id
            };

            var accountBalanceResponse = await _balanceClient
                .GetAccountBalanceAsync(request, cancellationToken: cancellationToken);

            if (accountBalanceResponse == null)
            {
                throw new Exception("Response is null");
            }

            accountModel.Balance = accountBalanceResponse.Balance;

            return accountModel;
        }

        public async Task UpdateAsync(int userId, int id, FinancialAccountActionModel updateModel, 
            CancellationToken cancellationToken)
        {
            var account = await _unitOfWork.FinancialAccounts.GetByIdAsync(id, cancellationToken);

            if (account == null)
            {
                throw new EntityNotFoundException("Account not found");
            }

            int receivedUserId = _consumer.ConsumeMessage(account.UserId);

            if (receivedUserId == 0 || userId != receivedUserId)
            {
                throw new Exception("The user is not logged in");
            }

            var updateAccount = _mapper.Map<FinancialAccount>(updateModel);
            updateAccount.Id = account.Id;

            await _unitOfWork.FinancialAccounts.UpdateAsync(id, updateAccount, cancellationToken);

            await _unitOfWork.SaveChangesAsync();

            await _cacheRepository.UpdateDataCacheAsync(id, updateModel);
        }
    }
}
