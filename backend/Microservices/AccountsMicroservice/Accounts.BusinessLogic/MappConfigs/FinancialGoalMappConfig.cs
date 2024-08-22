using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Entities;
using AutoMapper;

namespace Accounts.BusinessLogic.MappConfigs
{
    public class FinancialGoalMappConfig : Profile
    {
        public FinancialGoalMappConfig()
        {
            CreateMap<FinancialGoalModel, FinancialGoal>().ReverseMap();

            CreateMap<FinancialGoalActionModel, FinancialGoal>().ReverseMap();
        }
    }
}
