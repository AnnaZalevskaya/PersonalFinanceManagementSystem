using Accounts.BusinessLogic.Models;
using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
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
            PdfFont helveticaBoldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);
            PdfFont courierFont = PdfFontFactory.CreateFont(StandardFonts.COURIER);

            var elementsColor = new DeviceRgb(146, 101, 121);

            document.SetFontSize(20);

            var header = new Paragraph($"Financial account #{model.Id}:")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFont(helveticaBoldFont)
                .SetFontSize(32);
            document?.Add(header);

            var ls = new LineSeparator(new SolidLine());
            document?.Add(ls);

            document?.Add(new Paragraph()
                .Add("Name: ")
                .Add(new Text($"{model.Name}")
                    .SetFontColor(elementsColor))
                .SetFont(courierFont));

            document?.Add(new Paragraph()
                .Add("Type of financial account: ")
                .Add(new Text($"{model.AccountType.Name}")
                    .SetFontColor(elementsColor))
                .SetFont(courierFont));

            document?.Add(new Paragraph()
                .Add("Balance: ")
                .Add(new Text($"{model.Balance} {model.Currency.Sign}")
                    .SetFontColor(elementsColor))
                .SetFont(courierFont));

            document?.Add(new Paragraph()
                .Add("Currency: ")
                .Add(new Text($"{model.Currency.Name} ({model.Currency.Abbreviation})")
                    .SetFontColor(elementsColor))
                .SetFont(courierFont));
        }
    }
}
