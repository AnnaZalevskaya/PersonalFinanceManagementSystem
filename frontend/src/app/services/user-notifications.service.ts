import { EventEmitter, Injectable } from '@angular/core';
import { GoalAchievedNotification, UserNotification } from '../models/user-notification.model';
import { SignalRService } from './signal-r.service';

const NOTIFICATIONS_KEY = 'notifications';
const NOTIFICATIONS_COUNT_KEY = 'notifications_count';

@Injectable({
  providedIn: 'root'
})
export class UserNotificationsService {
  newNotificationReceived = new EventEmitter<number>();
  notificationsRead = new EventEmitter<void>();
  private notifications: UserNotification[] = [];
  private unreadCount = 0;

  constructor(private notificationService: SignalRService) {
    this.notificationService.getGoalAchievedNotificationStream()
      .subscribe((notification: GoalAchievedNotification) => {
        const newNotification: UserNotification = {
          userId: notification.userId,
          message: `Goal "${notification.goalName}" has been achieved!`,
          isRead: false
        };
        this.addNotification(newNotification);
      });
  }

  addNotification(notification: UserNotification) {
    this.notifications.push(notification);
    this.saveNotifications();
    this.unreadCount++;
    localStorage.setItem(NOTIFICATIONS_COUNT_KEY, this.unreadCount.toString());
    this.newNotificationReceived.emit(this.unreadCount);
  }

  getNotReadNotificationsCount() {
    return this.unreadCount;
  }

  getNotifications(): UserNotification[] {
    return this.notifications;
  }

  private saveNotifications() {
    localStorage.setItem(NOTIFICATIONS_KEY, JSON.stringify(this.notifications));
  }

  loadNotifications() {
    const storedNotifications = localStorage.getItem(NOTIFICATIONS_KEY);

    if (storedNotifications) {
      this.notifications = JSON.parse(storedNotifications);
      this.unreadCount = this.notifications.filter(notification => !notification.isRead).length;
    }

    return this.notifications;
  }
}