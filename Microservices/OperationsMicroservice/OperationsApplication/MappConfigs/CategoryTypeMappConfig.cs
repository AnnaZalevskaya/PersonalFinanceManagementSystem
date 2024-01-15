using AutoMapper;
using Operations.Application.Models;
using Operations.Core.Entities;

namespace Operations.Application.MappConfigs
{
    public class CategoryTypeMappConfig : Profile
    {
        public CategoryTypeMappConfig() {
            CreateMap<CategoryType, CategoryTypeModel>().ReverseMap();
        }
    }
}
