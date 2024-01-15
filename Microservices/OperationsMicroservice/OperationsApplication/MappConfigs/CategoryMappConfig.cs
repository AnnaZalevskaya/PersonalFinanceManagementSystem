using AutoMapper;
using MongoDB.Driver;
using Operations.Application.Models;
using Operations.Core.Entities;

namespace Operations.Application.MappConfigs
{
    public class CategoryMappConfig : Profile
    {
        public CategoryMappConfig()
        {
            CreateMap<MongoDBRef, CategoryTypeModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.CollectionName));

            CreateMap<Category, CategoryModel>()
                .ForMember(dest => dest.CategoryType, opt => opt.MapFrom(src => src.CategoryType))
                .ReverseMap();  
        }
    }
}
