using MediatR;

namespace Operations.Application.Operations.Commands.Reports.SaveReport
{
    public class SaveReportQuery : IRequest
    {
        public int AccountId { get; set; }
        public byte[] PdfBytes { get; set; }

        public SaveReportQuery(int accountId, byte[] pdfBytes)
        {
            AccountId = accountId;
            PdfBytes = pdfBytes;
        }
    }
}
