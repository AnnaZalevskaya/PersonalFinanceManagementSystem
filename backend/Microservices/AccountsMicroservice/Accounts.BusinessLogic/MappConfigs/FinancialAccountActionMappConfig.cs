using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Entities;
using AutoMapper;

namespace Accounts.BusinessLogic.MappConfigs
{
    public class FinancialAccountActionMappConfig : Profile
    {
        public FinancialAccountActionMappConfig()
        {
            CreateMap<FinancialAccountActionModel, FinancialAccount>().ReverseMap();
        }
    }
}
