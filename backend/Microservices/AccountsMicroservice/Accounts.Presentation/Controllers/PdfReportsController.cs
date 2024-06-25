using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Models.Consts;
using Accounts.BusinessLogic.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Accounts.Presentation.Controllers
{
    [ApiController]
    [Route("api/pdf-account-report")]
    public class PdfReportController : ControllerBase
    {
        private readonly IAccountPdfReportService _pdfReportService;

        public PdfReportController(IAccountPdfReportService pdfReportService)
        {
            _pdfReportService = pdfReportService;
        }

        [HttpPost("report/{accountId}")]
        [Authorize(Policy = AuthPolicyConsts.ClientOnly)]
        public async Task<ActionResult> GenerateReportAsync(int accountId, [FromBody] FinancialAccountModel model)
        {
            var file = await _pdfReportService.GeneratePdfReportFromAccountModel(model);

            return Ok(file);
        }
    }
}
