using AutoMapper;
using Operations.Application.Models;
using Operations.Core.Entities;

namespace Operations.Application.MappConfigs
{
    public class RecurringPaymentMappConfig : Profile
    {
        public RecurringPaymentMappConfig()
        {
            CreateMap<RecurringPayment, RecurringPaymentModel>().ReverseMap();

            CreateMap<RecurringPayment, RecurringPaymentActionModel>().ReverseMap();
        }
    }
}
