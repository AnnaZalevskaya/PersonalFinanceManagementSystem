using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Core.Entities;

namespace Operations.Application.Operations.Commands.ScheduleRecurringOperation.CreateRecurringPayment
{
    public class CreateRecurringPaymentCommandHandler : IRequestHandler<CreateRecurringPaymentCommand, string>
    {
        
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateRecurringPaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string> Handle(CreateRecurringPaymentCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<RecurringPayment>(command.Model);
            string id = await _unitOfWork.RecurringPayments.CreateAsync(entity, cancellationToken);

            return id;
        }
    }
}
