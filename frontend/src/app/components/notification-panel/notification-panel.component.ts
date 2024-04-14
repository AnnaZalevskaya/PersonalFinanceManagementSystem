import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { UserNotification } from '../../models/user-notification.model';
import { NotificationPanelService } from '../../services/notification-panel.service';

@Component({
  selector: 'app-notification-panel',
  standalone: true,
  imports: [
    CommonModule
  ],
  templateUrl: './notification-panel.component.html',
  styleUrl: './notification-panel.component.css'
})
export class NotificationPanelComponent implements OnInit {
  selectedNotification!: UserNotification; 
  notifications: UserNotification[] = [];
  isPanelOpen: boolean = false;

  constructor(private panelService: NotificationPanelService) { }

  ngOnInit(): void {
    this.notifications = this.panelService.outputNotifications();
  }

  closeNotificationPanel() {
    this.panelService.closePanel();
  }

  openNotification(notification: UserNotification) {
    this.selectedNotification = notification;
    notification.isRead = true; 
  }

  clearAllNotifications() {
    this.notifications = [];
  }
}
