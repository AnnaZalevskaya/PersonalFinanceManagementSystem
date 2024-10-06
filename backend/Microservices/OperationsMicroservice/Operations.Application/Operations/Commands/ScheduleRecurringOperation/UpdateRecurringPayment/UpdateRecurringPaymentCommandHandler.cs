using AutoMapper;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;
using Operations.Core.Entities;

namespace Operations.Application.Operations.Commands.ScheduleRecurringOperation.UpdateRecurringPayment
{
    public class UpdateRecurringPaymentCommandHandler : IRequestHandler<UpdateRecurringPaymentCommand, RecurringPaymentModel>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateRecurringPaymentCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }


        public async Task<RecurringPaymentModel> Handle(UpdateRecurringPaymentCommand command, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<RecurringPayment>(command.UpdatedPayment);

            await _unitOfWork.RecurringPayments
                .UpdateAsync(command.PaymentId, entity, cancellationToken);

            return _mapper.Map<RecurringPaymentModel>(entity);
        }
    }
}
