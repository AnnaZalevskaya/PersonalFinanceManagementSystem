﻿using MediatR;
using Operations.Application.Interfaces;

namespace Operations.Application.Operations.Commands.DeleteAccountOperations
{
    public class DeleteAccountOperationsCommandHandler : IRequestHandler<DeleteAccountOperationsCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteAccountOperationsCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteAccountOperationsCommand command, CancellationToken cancellationToken)
        {
            var operations = await _unitOfWork.Operations
                .GetByAccountIdAsync(command.AccountId,  command.PaginationSettings, cancellationToken);

            if (operations != null)
            {
                await _unitOfWork.Operations.DeleteByAccountIdAsync(command.AccountId, cancellationToken);
            }    
        }
    }
}
