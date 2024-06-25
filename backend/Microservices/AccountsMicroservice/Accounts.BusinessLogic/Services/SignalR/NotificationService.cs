using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Models.Consts;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Hosting;

namespace Accounts.BusinessLogic.Services.SignalR
{
    public class NotificationService : BackgroundService
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

        public async Task SendGoalAchievedNotification(string userId, string goalName)
        {
            var notification = new GoalAchievedNotification { UserId = userId, GoalName = goalName };
            await _notificationsHubContext.Clients.User(userId).SendAsync("ReceiveGoalAchievedNotification", notification);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {


            return Task.CompletedTask;
        }
    }
}
