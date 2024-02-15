using AutoMapper;
using MongoDB.Driver;
using Operations.Application.Models;
using Operations.Core.Entities;
using Operations.Core.Extensions;

namespace Operations.Application.MappConfigs
{
    public class CategoryMappConfig : Profile
    {
        public CategoryMappConfig()
        {
            CreateMap<Category, CategoryModel>()
                .ForMember(dest => dest.CategoryType, opt => opt.MapFrom(src => src.CategoryType))
                .ReverseMap();

            CreateMap<MongoDBRef, CategoryTypeModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.GetAsName<CategoryType>()))
                .ReverseMap();
        }
    }
}
