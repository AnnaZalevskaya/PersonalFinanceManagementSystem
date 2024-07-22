using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Commands.Reports.GenerateReport
{
    public class GenerateReportCommand : IRequest<byte[]>
    {
        public List<OperationModel> Models { get; set; }

        public GenerateReportCommand(List<OperationModel> models)
        {
            Models = models;
        }
    }
}
