using Accounts.BusinessLogic.Services.Interfaces;
using Google.Protobuf;
using gRPC.Protos.Server;
using Grpc.Core;

namespace Accounts.BusinessLogic.Services.gRPC
{
    public class PdfReportGrpcService : ReportBytes.ReportBytesBase
    {
        private readonly IAccountPdfReportService _reportService;
        private readonly IFinancialAccountService _accountService;

        public PdfReportGrpcService(IAccountPdfReportService reportService, IFinancialAccountService accountService)
        {
            _reportService = reportService;
            _accountService = accountService;

        }

        public async override Task<ReportBytesResponse> GetReportBytes(ReportBytesRequest request,
            ServerCallContext context)
        {
            var model = await _accountService.GetByIdAsync(request.AccountId, context.CancellationToken);
            var bytes = await _reportService.GeneratePdfReportFromAccountModel(model);

            var response = new ReportBytesResponse()
            {
                Bytes = ByteString.CopyFrom(bytes)
            };

            return await Task.FromResult(response);
        }
    }
}
