using iText.Kernel.Pdf;
using iText.Layout;
using MediatR;
using Operations.Application.Extensions;

namespace Operations.Application.Operations.Commands.Reports.GenerateReport
{
    public class GenerateReportQueryHandler : IRequestHandler<GenerateReportQuery, byte[]>
    {
        public async Task<byte[]> Handle(GenerateReportQuery command, CancellationToken cancellationToken)
        {
            var memoryStream = new MemoryStream();
            var writer = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(writer);
            var document = new Document(pdfDocument);

            document.ApplyDocHeaderContentAndStyle();

            foreach (var model in command.Models)
            {
                document.ApplyDocContentAndStyle(model);
            }

            document.Close();
            await Task.Yield();

            byte[] pdfBytes = memoryStream.ToArray();

            return pdfBytes;
        }
    }
}