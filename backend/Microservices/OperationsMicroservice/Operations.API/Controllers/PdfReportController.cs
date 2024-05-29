using MediatR;
using Microsoft.AspNetCore.Mvc;
using Operations.Application.Models;
using Operations.Application.Operations.Queries.Lists.GetOperationList;
using Operations.Application.Operations.Queries.Reports.GenerateReport;
using Operations.Application.Operations.Queries.Reports.MergeReports;
using Operations.Application.Operations.Queries.Reports.SaveReport;
using Operations.Application.Settings;

namespace Operations.API.Controllers
{
    [ApiController]
    [Route("api/pdf-reports")]
    public class PdfReportController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PdfReportController(IMediator mediator)
        {

            _mediator = mediator;

        }

        [HttpPost("report")]
        public async Task<ActionResult> GenerateReportAsync(int accountId)
        {
            var paginationSettings = new PaginationSettings()
            {
                PageSize = 3,
                PageNumber = 1
            };

            var operations = await _mediator
                .Send(new GetOperationListByAccountIdQuery(accountId, paginationSettings));
            var file = await _mediator.Send(new GenerateReportQuery(operations));

            return Ok(file);
        }

        [HttpPost]
        public async Task<IActionResult> MergeAndSavePdfFilesAsync([FromBody] MergedReportModel pdfBytes)
        {
            var mergedFile = await _mediator.Send(new MergeReportsQuery(pdfBytes.pdfBytesFile1, pdfBytes.pdfBytesFile2));
            await _mediator.Send(new SaveReportQuery(mergedFile));

            return Ok();
        }
    }
}
