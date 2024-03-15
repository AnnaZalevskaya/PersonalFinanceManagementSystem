using Auth.Application.Models;
using Auth.Core.Entities;
using AutoMapper;

namespace Auth.Application.MappConfigs
{
    public class AuthResponseMappConfig : Profile
    {
        public AuthResponseMappConfig()
        {
            CreateMap<AuthResponse, AppUser>().ReverseMap();
        }
    }
}
