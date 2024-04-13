import { Injectable } from '@angular/core';
import { UserNotificationsService } from './user-notifications.service';

@Injectable({
  providedIn: 'root'
})
export class NotificationPanelService {
  showNotificationPanel: boolean = false;

  constructor(private notificationsService: UserNotificationsService) { }
  
  openPanel() {
    this.showNotificationPanel = true;
  }
  
  closePanel() {
    this.showNotificationPanel = false;
  }

  getStatus() {
    return this.showNotificationPanel;
  }

  outputNotifications() {
    return this.notificationsService.getNotifications();
  }
}