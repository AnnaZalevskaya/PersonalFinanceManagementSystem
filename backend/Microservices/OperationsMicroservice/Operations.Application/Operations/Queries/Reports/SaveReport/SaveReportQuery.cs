using MediatR;

namespace Operations.Application.Operations.Queries.Reports.SaveReport
{
    public class SaveReportQuery : IRequest
    {
        public byte[] PdfBytes { get; set; }

        public SaveReportQuery(byte[] pdfBytes) 
        { 
            PdfBytes = pdfBytes;
        }
    }
}
