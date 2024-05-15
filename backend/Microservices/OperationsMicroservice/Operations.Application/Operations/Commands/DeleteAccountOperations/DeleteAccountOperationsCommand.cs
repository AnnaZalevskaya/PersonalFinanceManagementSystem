using MediatR;
using Operations.Application.Settings;

namespace Operations.Application.Operations.Commands.DeleteAccountOperations
{
    public class DeleteAccountOperationsCommand : IRequest
    {
        public int AccountId { get; set; }
        public PaginationSettings PaginationSettings { get; set; }

        public DeleteAccountOperationsCommand(int accountId)
        {
            AccountId = accountId;
            PaginationSettings = new PaginationSettings
            {
                PageNumber = 1,
                PageSize = 1
            };
        }
    }
}
