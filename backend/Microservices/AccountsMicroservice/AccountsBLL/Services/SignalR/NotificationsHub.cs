using Accounts.BusinessLogic.Models.Consts;
using Microsoft.AspNetCore.SignalR;

namespace Accounts.BusinessLogic.Services.SignalR
{
    public class NotificationsHub : Hub
    {
        public async Task SendNotificationToUser(string userId, string message)
        {
            await Clients.User(userId).SendAsync(NotificationSettingsConsts.MethodName, message);
        }
    }
}
