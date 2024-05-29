using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using MediatR;

namespace Operations.Application.Operations.Queries.Reports.GenerateReport
{
    public class GenerateReportQueryHandler : IRequestHandler<GenerateReportQuery, byte[]>
    {
        public async Task<byte[]> Handle(GenerateReportQuery query, CancellationToken cancellationToken)
        {
            var memoryStream = new MemoryStream();
            var writer = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(writer);
            var document = new Document(pdfDocument);

            foreach(var model in query.Models)
            {
                document.Add(new Paragraph(":"));
                document.Add(new Paragraph($"Name: {model.Date}"));
                document.Add(new Paragraph());
            }

            document.Close();
            await Task.Yield();

            byte[] pdfBytes = memoryStream.ToArray();  

            return pdfBytes;
        }
    }
}