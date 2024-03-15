import { EventEmitter, Injectable } from '@angular/core';
import { UserNotification } from '../models/user-notification.model';

const NOTIFICATIONS_KEY = 'notifications';
const NOTIFICATIONS_COUNT_KEY = 'notifications_count';

@Injectable({
  providedIn: 'root'
})
export class UserNotificationsService {
  newNotificationReceived = new EventEmitter<number>;
  notificationsRead = new EventEmitter<void>;
  private notifications: UserNotification[] = [];
  private unreadCount = 0;

  addNotification(notification: UserNotification) {
    this.notifications.push(notification);
    this.saveNotifications();
    this.unreadCount++;
    localStorage.setItem(NOTIFICATIONS_COUNT_KEY, this.unreadCount.toString());
  }

  getNotReadNotificationsCount() {
    const data = localStorage.getItem(NOTIFICATIONS_KEY);

    if(!data) {
      return 0;
    }
    const count = localStorage.getItem(NOTIFICATIONS_COUNT_KEY)

    return count;
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
    }

    return this.notifications;
  } 
}
