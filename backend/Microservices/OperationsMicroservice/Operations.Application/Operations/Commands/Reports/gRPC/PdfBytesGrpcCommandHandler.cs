using gRPC.Protos.Client;
using MediatR;
using static gRPC.Protos.Client.ReportBytes;

namespace Operations.Application.Operations.Commands.Reports.gRPC
{
    public class PdfBytesGrpcCommandHandler : IRequestHandler<PdfBytesGrpcCommand, byte[]>
    {
        private readonly ReportBytesClient _reportClient;

        public PdfBytesGrpcCommandHandler(ReportBytesClient reportClient)
        {
            _reportClient = reportClient;
        }

        public async Task<byte[]> Handle(PdfBytesGrpcCommand command, CancellationToken cancellationToken)
        {
            var request = new ReportBytesRequest()
            {
                AccountId = command.AccountId
            };

            var bytes = await _reportClient
                .GetReportBytesAsync(request, cancellationToken: cancellationToken);

            return bytes.Bytes.ToByteArray();
        }
    }
}
