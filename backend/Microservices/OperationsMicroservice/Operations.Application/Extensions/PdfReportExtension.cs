using iText.IO.Font.Constants;
using iText.Kernel.Colors;
using iText.Kernel.Font;
using iText.Kernel.Pdf.Canvas.Draw;
using iText.Layout;
using iText.Layout.Element;
using iText.Layout.Properties;
using Operations.Application.Models;

namespace Operations.Application.Extensions
{
    public static class PdfReportExtension
    {
        public static void ApplyDocContentAndStyle(this Document document, OperationModel model, List<CategoryModel> categoryModels)
        {
            PdfFont helveticaFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA);
            PdfFont helveticaBoldFont = PdfFontFactory.CreateFont(StandardFonts.HELVETICA_BOLD);

            PdfFont courierFont = PdfFontFactory.CreateFont(StandardFonts.COURIER);
            PdfFont courierBoldFont = PdfFontFactory.CreateFont(StandardFonts.COURIER_BOLD);

            var elementsColor = new DeviceRgb(110, 142, 236);

            var operation = new Paragraph()
                .Add("FO ")
                .Add(new Text($"{model.Id}")
                    .SetFontColor(elementsColor)
                    .SetFont(helveticaFont)
                    .SetBold())
                .Add(":")
                .SetFontSize(24)
                .SetMarginTop(12);
            document?.Add(operation);

            var title = new Paragraph("Description:")
                .SetFontSize(20)
                .SetFont(courierBoldFont);
            document?.Add(title);

            foreach (var record in model.Description)
            {
                if (record.Key == "CategoryId")
                {
                    document?.Add(new Paragraph()
                        .Add("Category - ")
                        .Add(new Text($"{categoryModels.Find(model => model.Id == (int)record.Value).Name}")
                            .SetFontColor(elementsColor))
                        .SetFont(courierFont));
                }
                else
                {
                    document?.Add(new Paragraph()
                        .Add($"{record.Key} - ")
                        .Add(new Text($"{record.Value}")
                            .SetFontColor(elementsColor))
                        .SetFont(courierFont));
                }
                var ls = new LineSeparator(new SolidLine());
                document?.Add(ls);
            }

            document?.Add(new Paragraph(new Text(" ")));
        }

        public static void ApplyDocHeaderContentAndStyle(this Document document)
        {
            var header = new Paragraph("Recent financial operations:")
                .SetTextAlignment(TextAlignment.CENTER)
                .SetFontSize(28);
            document?.Add(header);

            document?.Add(new Paragraph(new Text(" ")));
        }

        public static void ApplyNoOperationsContentAndStyle(this Document document)
        {
            var message = new Paragraph(
                new Text("This account has not have any financial operations yet.")
                    .SetTextAlignment(TextAlignment.RIGHT)
                    .SetFontSize(24)
                    .SetItalic()
                )
                .SetMarginTop(24);
            document.Add(message);
        }
    }
}

