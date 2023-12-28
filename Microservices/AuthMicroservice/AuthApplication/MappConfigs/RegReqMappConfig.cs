using Auth.Application.Models;
using Auth.Core.Entities;
using AutoMapper;

namespace Auth.Application.MappConfigs
{
    public class RegReqMappConfig : Profile
    {
        public RegReqMappConfig() 
        {
            CreateMap<RegisterRequest, AppUser>().ReverseMap();
        }
    }
}
