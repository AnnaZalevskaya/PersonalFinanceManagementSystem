import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { UserNotification } from '../../../models/user-notification.model';
import { NotificationPanelService } from '../../../services/notification-panel.service';
import { UserNotificationsService } from '../../../services/user-notifications.service';
import { MatDividerModule } from '@angular/material/divider';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-notification-panel',
  standalone: true,
  imports: [
    CommonModule,
    MatIconModule,
    MatDividerModule
  ],
  templateUrl: './notification-panel.component.html',
  styleUrls: ['./notification-panel.component.css']
})
export class NotificationPanelComponent implements OnInit {
  selectedNotification!: UserNotification;
  notifications: UserNotification[] = [];
  isPanelOpen: boolean = false;

  constructor(
    private panelService: NotificationPanelService,
    private userNotificationsService: UserNotificationsService
  ) { }

  ngOnInit(): void {
    /*
    this.notifications = this.userNotificationsService.getNotifications();
    this.userNotificationsService.newNotificationReceived.subscribe(() => {
      this.notifications = this.userNotificationsService.getNotifications();
    });*/
  }

  closeNotificationPanel() {
    this.panelService.closePanel();
  }

  openNotification(notification: UserNotification) {
    this.selectedNotification = notification;
    notification.isRead = true;
    this.userNotificationsService.notificationsRead.emit();
  }

  clearAllNotifications() {
    this.notifications = [];
  }
}