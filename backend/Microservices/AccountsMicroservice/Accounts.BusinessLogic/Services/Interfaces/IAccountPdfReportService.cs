using Accounts.BusinessLogic.Models;

namespace Accounts.BusinessLogic.Services.Interfaces
{
    public interface IAccountPdfReportService
    {
        Task<byte[]> GeneratePdfReportFromAccountModel(FinancialAccountModel model);
    }
}
