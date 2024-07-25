using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Commands.Reports.MergeReports
{
    public class MergeReportsCommand : IRequest<byte[]>
    {
        public MergedReportModel MergedReport { get; set; }

        public MergeReportsCommand(MergedReportModel mergedReport)
        {
            MergedReport = mergedReport;
        }
    }
}
