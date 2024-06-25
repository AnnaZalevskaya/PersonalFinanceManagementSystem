using Accounts.BusinessLogic.Models;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;

namespace Accounts.BusinessLogic.Extensions
{
    public static class PdfReportExtension
    {
        public static void ApplyDocContentAndStyle(this Document document, FinancialAccountModel model)
        {
            document.SetFontSize(24);

            var header = new Paragraph($"Financial account num.{model.Id}:")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(32);
            document?.Add(header);

            var ls = new LineSeparator(new SolidLine());
            document?.Add(ls);

            document?.Add(new Paragraph($"Name: {model.Name}"));
            document?.Add(new Paragraph($"Type of financial account: {model.AccountType.Name}"));
            document?.Add(new Paragraph($"Balance: {model.Balance} {model.Currency.Sign}"));
            document?.Add(new Paragraph($"Currency: {model.Currency.Name} ({model.Currency.Abbreviation})"));
        }
    }
}
