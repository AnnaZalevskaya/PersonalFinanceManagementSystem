using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Dapper.Entities;
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
