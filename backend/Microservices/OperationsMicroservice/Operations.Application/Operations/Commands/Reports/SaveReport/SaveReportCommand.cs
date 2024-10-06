using MediatR;

namespace Operations.Application.Operations.Commands.Reports.SaveReport
{
    public class SaveReportCommand : IRequest
    {
        public int AccountId { get; set; }
        public byte[] PdfBytes { get; set; }

        public SaveReportCommand(int accountId, byte[] pdfBytes)
        {
            AccountId = accountId;
            PdfBytes = pdfBytes;
        }
    }
}
