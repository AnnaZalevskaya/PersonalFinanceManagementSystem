import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { UserNotificationsService } from '../../services/user-notifications.service';
import { UserNotification } from '../../models/user-notification.model';

@Component({
  selector: 'app-notifications',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './notifications.component.html',
  styleUrl: './notifications.component.css'
})
export class NotificationsComponent implements OnInit {
  notifications!: UserNotification[];

  constructor(private notificationsService: UserNotificationsService) {}

  ngOnInit(): void {
    this.loadUnreadCount();
  }

  loadUnreadCount() {
    this.notifications = this.notificationsService.getNotifications();
  }
}
