using AutoMapper;
using Operations.Application.Models;
using Operations.Core.Entities;

namespace Operations.Application.MappConfigs
{
    public class CategoryMappConfig : Profile
    {
        public CategoryMappConfig()
        {
            CreateMap<Category, CategoryModel>().ReverseMap();
        }
    }
}
