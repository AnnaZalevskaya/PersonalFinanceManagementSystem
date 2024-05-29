using Accounts.BusinessLogic.Models;

namespace Accounts.BusinessLogic.Services.Interfaces
{
    public interface IAccountPdfReportService
    {
        void GeneratePdfReportFromModel(FinancialAccountModel model);
        Task<byte[]> GeneratePdfReportFromAccountModel(FinancialAccountModel model);
    }
}
