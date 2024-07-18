
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.Models;
using Operations.Application.Models.Consts;
using Operations.Application.Operations.Commands.Reports.GenerateReport;
using Operations.Application.Operations.Commands.Reports.gRPC;
using Operations.Application.Operations.Commands.Reports.MergeReports;
using Operations.Application.Operations.Commands.Reports.SaveReport;
using Operations.Application.Operations.Queries.Lists.GetOperationList;
using Operations.Application.Operations.Queries.RecordsCount.GetAccountOperationRecordsCount;
using Operations.Application.Settings;

namespace Operations.API.Controllers
{
    [ApiController]
    [Route("api/pdf-operations-report")]
    public class PdfReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PdfReportController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("report/{accountId}")]
        [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult<byte[]>> GenerateReportAsync(int accountId)
        {
            var paginationSettings = new PaginationSettings()
            {
                PageSize = (int)await _mediator
                    .Send(new GetAccountOperationRecordsCountQuery(accountId)),
                PageNumber = 1
            };

            var operations = await _mediator
                .Send(new GetOperationListByAccountIdQuery(accountId, paginationSettings));
            var file = await _mediator.Send(new GenerateReportQuery(operations));

            return Ok(file);
        }

        [HttpPost]
        [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<IActionResult> MergeAndSavePdfFilesAsync(int accountId, 
            [FromBody] MergedReportModel pdfBytes)
        {
            var mergedFile = await _mediator.Send(new MergeReportsQuery(pdfBytes));
            await _mediator.Send(new SaveReportQuery(accountId, mergedFile));

            return NoContent();
        }

        [HttpPost("full-report/{accountId}")]
       // [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<IActionResult> SavePdfFilesAsync(int accountId)
        {
            var bytes1 = await _mediator.Send(new PdfBytesGrpcCommand(accountId));

            var paginationSettings = new PaginationSettings()
            {
                PageSize = (int)await _mediator
                    .Send(new GetAccountOperationRecordsCountQuery(accountId)),
                PageNumber = 1
            };

            var operations = await _mediator
                .Send(new GetOperationListByAccountIdQuery(accountId, paginationSettings));
            var bytes2 = await _mediator.Send(new GenerateReportQuery(operations));

            var pdfBytes = new MergedReportModel()
            {
                PdfBytesFile1 = bytes1,
                PdfBytesFile2 = bytes2
            };

            var mergedFile = await _mediator.Send(new MergeReportsQuery(pdfBytes));
            await _mediator.Send(new SaveReportQuery(accountId, mergedFile));

            return NoContent();
        }
    }
}
