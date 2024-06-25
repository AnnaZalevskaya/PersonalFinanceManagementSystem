using Accounts.BusinessLogic.Extensions;
using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class AccountPdfReportService : IAccountPdfReportService
    {
        public async Task<byte[]> GeneratePdfReportFromAccountModel(FinancialAccountModel model)
        {
            var memoryStream = new MemoryStream();

            using (var writer = new PdfWriter(memoryStream))
            {
                using (var pdfDocument = new PdfDocument(writer))
                {
                    var document = new Document(pdfDocument);

                    document.ApplyDocContentAndStyle(model);

                    document.Close();
                }
            }

            await Task.Yield();

            byte[] pdfBytes = memoryStream.ToArray();

            return pdfBytes;
        }
    }
}
