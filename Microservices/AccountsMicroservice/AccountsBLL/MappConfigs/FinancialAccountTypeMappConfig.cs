using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Entities;
using AutoMapper;

namespace Accounts.BusinessLogic.MappConfigs
{
    public class FinancialAccountTypeMappConfig : Profile
    {
        public FinancialAccountTypeMappConfig()
        {
            CreateMap<FinancialAccountTypeModel, FinancialAccountType>().ReverseMap();
        }
    }
}
