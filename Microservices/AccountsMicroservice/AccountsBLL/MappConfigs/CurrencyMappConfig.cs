using Accounts.BusinessLogic.Models;
using Accounts.DataAccess.Entities;
using AutoMapper;

namespace Accounts.BusinessLogic.MappConfigs
{
    public class CurrencyMappConfig : Profile 
    {
        public CurrencyMappConfig()
        {
            CreateMap<CurrencyModel, Currency>().ReverseMap();
        }
    }
}
