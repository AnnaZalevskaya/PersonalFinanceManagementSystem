﻿using Accounts.BusinessLogic.Models;
using Accounts.BusinessLogic.Models.Consts;
using Microsoft.AspNetCore.SignalR;

namespace Accounts.BusinessLogic.Services.SignalR
{
    public class NotificationsHub : Hub
    {
        public async Task SendNotificationToUser(string userId, string message)
        {
            await Clients.Client(this.Context.ConnectionId).SendAsync(NotificationSettingsConsts.MethodName, message);
        }

        public void ReceiveGoalAchievedNotification(GoalAchievedNotification notification)
        {
            Clients.User(notification.UserId).SendAsync("GoalAchieved", notification.GoalName);
        }
    }
}
