﻿using AutoMapper;
using MediatR;
using Operations.Application.Consumers;
using Operations.Application.Interfaces;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.GetOperationList
{
    public class GetOperationListByAccountIdQueryHandler : IRequestHandler<GetOperationListByAccountIdQuery, 
        List<OperationModel>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IMessageConsumer _consumer;
        private readonly ICacheRepository _cacheRepository;

        public GetOperationListByAccountIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper, 
            IMessageConsumer consumer, ICacheRepository cacheRepository)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _consumer = consumer;
            _cacheRepository = cacheRepository;
        }

        public async Task<List<OperationModel>> Handle(GetOperationListByAccountIdQuery query,
            CancellationToken cancellationToken)
        {
            //int id = _consumer.ConsumeMessage(query.AccountId);

            //if (id == 0)
            //{
            //    throw new Exception("The user's account was not found");
            //}

            var cachedOperations = await _cacheRepository
                .GetCachedLargeDataAsync<OperationModel>($"all_account_operations_{query.AccountId}" +
                $"_{query.paginationSettings.PageNumber}_{query.paginationSettings.PageSize}");

            if (cachedOperations.Count != 0)
            {
                return cachedOperations;
            }

            var entities = await _unitOfWork.Operations
                .GetByAccountIdAsync(query.AccountId, query.paginationSettings, cancellationToken);
            var operationsList = _mapper.Map<List<OperationModel>>(entities);

            await _cacheRepository
                .CacheLargeDataAsync($"all_account_operations_{query.AccountId}_{query.paginationSettings.PageNumber}" +
                $"_{query.paginationSettings.PageSize}", operationsList);

            return operationsList;
        }
    }
}
