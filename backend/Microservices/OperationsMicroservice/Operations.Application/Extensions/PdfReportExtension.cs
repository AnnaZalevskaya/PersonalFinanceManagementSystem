using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Operations.Application.Models;

namespace Operations.Application.Extensions
{
    public static class PdfReportExtension
    {
        public static void ApplyDocContentAndStyle(this Document document, OperationModel model)
        {
            var operation = new Paragraph($"Financial operation № {model.Id}:")
                .SetFontSize(26);
            document?.Add(operation);

            var title = new Paragraph("Description:")
                .SetFontSize(26);
            document?.Add(title);

            foreach (var record in model.Description)
            {
                document?.Add(new Paragraph($"{record.Key} - {record.Value}"));
                var ls = new LineSeparator(new SolidLine());
                document?.Add(ls);
            }

            var slF = new LineSeparator(new SolidLine()).SetWidth(3);
            document?.Add(slF);
        }

        public static void ApplyDocHeaderContentAndStyle(this Document document)
        {
            document.SetFontSize(24);

            var header = new Paragraph("Recent financial operations:")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(32);
            document?.Add(header);

            var slH = new LineSeparator(new SolidLine()).SetWidth(3);
            document?.Add(slH);
        }
    }
}

