using Operations.Application.Models;

namespace Operations.Application.MassTransit.Responses
{
    public class GetOperationsResponse
    {
        public List<OperationModel> Operations { get; set; }
        public int Count { get; set; }
    }
}
