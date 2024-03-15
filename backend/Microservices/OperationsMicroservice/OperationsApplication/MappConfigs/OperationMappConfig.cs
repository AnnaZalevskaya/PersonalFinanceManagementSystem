using AutoMapper;
using Operations.Application.Models;
using Operations.Core.Entities;

namespace Operations.Application.MappConfigs
{
    public class OperationMappConfig : Profile
    {
        public OperationMappConfig()
        {
            CreateMap<Operation, OperationModel>().ReverseMap();

            CreateMap<Operation, CreateOperationModel>().ReverseMap();
        }
    }
}
