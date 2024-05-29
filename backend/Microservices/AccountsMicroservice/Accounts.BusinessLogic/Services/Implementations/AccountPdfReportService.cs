using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Services.Interfaces;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class AccountPdfReportService : IAccountPdfReportService
    {
        public void GeneratePdfReportFromModel(FinancialAccountModel model)
        {
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            string outputPath = Path.Combine(downloadsPath, $"FinancialAccount_{model.Id}.pdf");

            using (var writer = new PdfWriter(outputPath))
            {
                using (var pdfDocument = new PdfDocument(writer))
                {
                    var document = new Document(pdfDocument);

                    document
                        .SetFontSize(24);

                    var header = new Paragraph($"Financial account num.{model.Id}:")
                        .SetTextAlignment(TextAlignment.CENTER)
                        .SetFontSize(20);
                    document.Add(header);
                    var ls = new LineSeparator(new SolidLine());
                    document.Add(ls);
                    document.Add(new Paragraph($"Name: {model.Name}"));
                    document.Add(new Paragraph($"Type of financial account: {model.AccountType.Name}"));
                    document.Add(new Paragraph($"Balance: {model.Balance} {model.Currency.Abbreviation}"));
                    document.Add(new Paragraph($"Currency: {model.Currency.Name}"));

                    document.Close();
                }
            }
        }

        public async Task<byte[]> GeneratePdfReportFromAccountModel(FinancialAccountModel model)
        {
            var memoryStream = new MemoryStream();

            using (var writer = new PdfWriter(memoryStream))
            {
                using (var pdfDocument = new PdfDocument(writer))
                {
                    var document = new Document(pdfDocument);

                    var header = new Paragraph("Financial account:");
                    document.Add(header);
                    document.Add(new Paragraph($"Name: {model?.Name}"));
                    document.Add(new Paragraph($"Type of financial account: {model?.AccountType.Name}"));
                    document.Add(new Paragraph($"Balance: {model?.Balance}"));

                    document.Close();
                }
            }

            await Task.Yield();

            byte[] pdfBytes = memoryStream.ToArray();

            return pdfBytes;
        }
    }
}
