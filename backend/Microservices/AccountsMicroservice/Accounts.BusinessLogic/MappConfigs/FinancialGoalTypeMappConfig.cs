using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Entities;
using AutoMapper;

namespace Accounts.BusinessLogic.MappConfigs
{
    public class FinancialGoalTypeMappConfig : Profile
    {
        public FinancialGoalTypeMappConfig()
        {
            CreateMap<FinancialGoalTypeModel, FinancialGoalType>().ReverseMap();
        }
    }
}
