using AutoMapper;
using Hangfire;
using MediatR;
using Operations.Application.Interfaces;
using Operations.Application.Models;
using Operations.Application.Models.Enums;

namespace Operations.Application.Operations.Commands.ScheduleRecurringOperation.AddOrUpdateRecurring
{
    public class RecurringCommandHandler : IRequestHandler<RecurringCommand>
    {
        private readonly IBackgroundJobClient _backgroundJobClient;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RecurringCommandHandler(IBackgroundJobClient backgroundJobClient, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _backgroundJobClient = backgroundJobClient;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Handle(RecurringCommand command, CancellationToken cancellationToken)
        {
            var payment = await _unitOfWork.RecurringPayments.GetAsync(command.Id, cancellationToken);
            var model = _mapper.Map<RecurringPaymentModel>(payment);

            RecurringJob.AddOrUpdate<PaymentProcessor>(
                model.Id,
                processor => processor.ProcessPayment(model, cancellationToken),
                GetCronExpressionForInterval(model.IntervalType, model.ExecutionTime),
                TimeZoneInfo.Local
            );
        }

        private string GetCronExpressionForInterval(int interval, TimeOnly time)
        {
            string cronTemplate = "";

            switch (interval)
            {
                case (int)IntervalEnum.Daily:
                    cronTemplate = $"{time.Minute} {time.Hour} * * *";
                    break;
                case (int)IntervalEnum.Weekly:
                    cronTemplate = $"{time.Minute} {time.Hour} * * {(int)DateTime.Today.DayOfWeek}";
                    break;
                case (int)IntervalEnum.OnceEveryTwoWeek:
                    cronTemplate = $"{time.Minute} {time.Hour} */2 * *";
                    break;
                case (int)IntervalEnum.Monthly:
                    cronTemplate = $"{time.Minute} {time.Hour} 1 * *";
                    break;
                case (int)IntervalEnum.Yearly:
                    cronTemplate = $"{time.Minute} {time.Hour} 1 1 *";
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(interval), "Invalid interval");
            }

            return cronTemplate;
        }
    }
}
