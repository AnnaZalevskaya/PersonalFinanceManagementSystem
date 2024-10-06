using Microsoft.AspNetCore.SignalR;

namespace Operations.Application.Operations.Commands.SignalR
{
    public class ReportHub : Hub
    {
        public async Task SendReportNotification(string userId)
        {
            await Clients.User(userId).SendAsync("ReportSaved");
        }
    }
}
