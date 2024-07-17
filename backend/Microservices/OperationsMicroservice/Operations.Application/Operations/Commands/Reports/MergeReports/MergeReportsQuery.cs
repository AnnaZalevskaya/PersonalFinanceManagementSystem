using MediatR;
using Operations.Application.Models;

namespace Operations.Application.Operations.Commands.Reports.MergeReports
{
    public class MergeReportsQuery : IRequest<byte[]>
    {
        public MergedReportModel MergedReport { get; set; }

        public MergeReportsQuery(MergedReportModel mergedReport)
        {
            MergedReport = mergedReport;
        }
    }
}
