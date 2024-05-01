using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Entities;
using AutoMapper;

namespace Accounts.BusinessLogic.MappConfigs
{
    public class FinancialAccountMappConfig : Profile 
    {
        public FinancialAccountMappConfig()
        {
            CreateMap<FinancialAccountModel, FinancialAccount>().ReverseMap();
        }
    }
}
