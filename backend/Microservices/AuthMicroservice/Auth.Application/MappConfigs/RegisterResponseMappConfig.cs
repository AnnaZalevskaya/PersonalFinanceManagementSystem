using Auth.Application.Models;
using Auth.Core.Entities;
using AutoMapper;

namespace Auth.Application.MappConfigs
{
    public class RegisterResponseMappConfig : Profile
    {
        public RegisterResponseMappConfig()
        {
            CreateMap<RegisterResponseModel, AppUser>().ReverseMap();
        }
    }
}
