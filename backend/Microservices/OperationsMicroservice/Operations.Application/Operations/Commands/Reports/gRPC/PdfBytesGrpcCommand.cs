using MediatR;

namespace Operations.Application.Operations.Commands.Reports.gRPC
{
    public class PdfBytesGrpcCommand : IRequest<byte[]>
    {
        public int AccountId { get; set; }

        public PdfBytesGrpcCommand(int accountId)
        {
            AccountId = accountId;
        }
    }
}
