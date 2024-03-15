using Auth.Application.Models;
using Auth.Core.Entities;
using AutoMapper;

namespace Auth.Application.MappConfigs
{
    public class UserModelMappConfig : Profile
    {
        public UserModelMappConfig()
        {
            CreateMap<UserModel, AppUser>().ReverseMap();
        }
    }
}
