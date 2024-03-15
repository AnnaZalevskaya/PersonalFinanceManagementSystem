using Accounts.BusinessLogic.Models.Consts;
using Accounts.BusinessLogic.Services.Interfaces;
using Accounts.BusinessLogic.Services.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace Accounts.BusinessLogic.Services.Implementations
{
    public class NotificationService :INotificationService
    {
        private readonly IHubContext<NotificationsHub> _notificationsHubContext;

        public NotificationService(IHubContext<NotificationsHub> notificationHubContext)
        {
            _notificationsHubContext = notificationHubContext;
        }

        public async Task SendNotificationAsync(string userId, string message)
        {
            await _notificationsHubContext.Clients.User(userId).SendAsync(NotificationSettingsConsts.MethodName, message);
        }
    }

}
