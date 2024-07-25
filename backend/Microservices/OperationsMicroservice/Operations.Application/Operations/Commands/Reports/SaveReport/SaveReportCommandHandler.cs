using MediatR;

namespace Operations.Application.Operations.Commands.Reports.SaveReport
{
    public class SaveReportCommandHandler : IRequestHandler<SaveReportCommand>
    {
        public Task Handle(SaveReportCommand command, CancellationToken cancellationToken)
        {
            string downloadsPath = Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads";
            string baseFileName = $"FinancialAccount_{command.AccountId}.pdf";
            string filePath = Path.Combine(downloadsPath, baseFileName);

            int counter = 1;
            string newFileName = baseFileName;

            while (File.Exists(filePath))
            {
                newFileName = $"{Path.GetFileNameWithoutExtension(baseFileName)} ({counter}){Path.GetExtension(baseFileName)}";
                filePath = Path.Combine(downloadsPath, newFileName);
                counter++;
            }
            
            File.WriteAllBytes(filePath, command.PdfBytes);

            return Task.CompletedTask;
        }
    }
}
