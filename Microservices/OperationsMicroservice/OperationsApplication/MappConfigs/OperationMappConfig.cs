using AutoMapper;
using Operations.Application.Models;
using Operations.Application.Operations.Commands.CreateOperation;
using Operations.Core.Entities;

namespace Operations.Application.MappConfigs
{
    public class OperationMappConfig : Profile
    {
        public OperationMappConfig()
        {
            CreateMap<Operation, OperationModel>().ReverseMap();

            CreateMap<Operation, CreateOperationCommand>().ReverseMap();
        }
    }
}
