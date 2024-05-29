using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Queries.Reports.GenerateReport
{
    public class GenerateReportQuery : IRequest<byte[]>
    {
        public List<OperationModel> Models { get; set; }

        public GenerateReportQuery(List<OperationModel> models)
        {
            Models = models;
        }
    }
}
