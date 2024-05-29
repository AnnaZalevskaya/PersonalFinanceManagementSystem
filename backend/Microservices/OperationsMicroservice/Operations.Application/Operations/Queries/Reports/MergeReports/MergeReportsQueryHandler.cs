using iText.Kernel.Pdf;
using iText.Kernel.Utils;
using MediatR;

namespace Operations.Application.Operations.Queries.Reports.MergeReports
{
    public class MergeReportsQueryHandler : IRequestHandler<MergeReportsQuery, byte[]>
    {
        public async Task<byte[]> Handle(MergeReportsQuery query, CancellationToken cancellationToken)
        {
            var memoryStream = new MemoryStream();
            var writer = new PdfWriter(memoryStream);
            var pdfDocument = new PdfDocument(writer);
            var merger = new PdfMerger(pdfDocument);

            var pdfReader1 = new PdfReader(new MemoryStream(query.PdfBytes1));
            var sourceDocument1 = new PdfDocument(pdfReader1);

            merger.Merge(sourceDocument1, 1, sourceDocument1.GetNumberOfPages());

            var pdfReader2 = new PdfReader(new MemoryStream(query.PdfBytes2));
            var sourceDocument2 = new PdfDocument(pdfReader2);

            merger.Merge(sourceDocument2, 1, sourceDocument2.GetNumberOfPages());

            sourceDocument1.Close();
            sourceDocument2.Close();

            merger.Close();
            pdfDocument.Close();

            await Task.Yield();

            return memoryStream.ToArray();
        }
    }
}
