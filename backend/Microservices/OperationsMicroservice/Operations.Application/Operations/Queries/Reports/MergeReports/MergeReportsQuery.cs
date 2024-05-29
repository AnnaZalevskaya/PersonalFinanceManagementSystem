using MediatR;

namespace Operations.Application.Operations.Queries.Reports.MergeReports
{
    public class MergeReportsQuery : IRequest<byte[]>
    {
        public byte[] PdfBytes1 { get; set; }
        public byte[] PdfBytes2 { get; set; }

        public MergeReportsQuery(byte[] pdfBytes1, byte[] pdfBytes2) 
        {
            PdfBytes1 = pdfBytes1;
            PdfBytes2 = pdfBytes2;
        }
    }
}
