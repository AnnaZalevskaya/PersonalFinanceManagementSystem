using Auth.Application.Models;
using Auth.Core.Entities;
using AutoMapper;

namespace Auth.Application.MappConfigs
{
    public class RegisterRequestMappConfig : Profile
    {
        public RegisterRequestMappConfig() 
        {
            CreateMap<RegisterRequestModel, AppUser>().ReverseMap();
        }
    }
}
