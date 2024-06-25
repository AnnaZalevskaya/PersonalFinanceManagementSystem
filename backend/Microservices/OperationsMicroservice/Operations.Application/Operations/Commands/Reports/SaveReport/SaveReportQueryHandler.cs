using MediatR;

namespace Operations.Application.Operations.Commands.Reports.SaveReport
{
    public class SaveReportQueryHandler : IRequestHandler<SaveReportQuery>
    {
        public Task Handle(SaveReportQuery command, CancellationToken cancellationToken)
        {
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            string filePath = Path.Combine(downloadsPath, $"FinancialAccount_{command.AccountId}.pdf");

            File.WriteAllBytes(filePath, command.PdfBytes);

            return Task.CompletedTask;
        }
    }
}
