using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Entities;
using AutoMapper;

namespace Accounts.BusinessLogic.MappConfigs
{
    public class AccountStatisticsMappConfig : Profile
    {
        public AccountStatisticsMappConfig() 
        {
            CreateMap<AccountStatisticsModel, AccountStatistics>().ReverseMap();
        }
    }
}
