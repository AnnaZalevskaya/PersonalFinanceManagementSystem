using MediatR;

namespace Operations.Application.Operations.Queries.Reports.SaveReport
{
    public class SaveReportQueryHandler : IRequestHandler<SaveReportQuery>
    {
        public Task Handle(SaveReportQuery query, CancellationToken cancellationToken)
        {
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            string filePath = Path.Combine(downloadsPath, "FinancialAccount.pdf");

            File.WriteAllBytes(filePath, query.PdfBytes);

            return Task.CompletedTask;
        }
    }
}
