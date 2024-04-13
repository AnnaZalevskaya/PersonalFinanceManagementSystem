import { Injectable } from '@angular/core';
import { HubConnectionBuilder, HubConnection, HubConnectionState } from '@microsoft/signalr';
import { UserNotification } from '../models/user-notification.model';
import { UserNotificationsService } from './user-notifications.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: HubConnection;

  constructor(private notificationsService: UserNotificationsService) {}

  startConnection(): void {
    this.hubConnection = new HubConnectionBuilder()
      .withUrl('https://localhost:44304/notifications')
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.error('Error while starting connection: ' + err));
  }

  startNotificationsListener(): void {
    this.hubConnection.on('ReceiveNotification', message => {
      const notification: UserNotification = { message: message, isRead: false };
      this.notificationsService.addNotification(notification);
    });
  }

  sendNotification(userId: string, message: string): void {
    if (this.hubConnection && this.hubConnection.state === HubConnectionState.Connected) {
      this.hubConnection.invoke("ReceiveNotification", userId, message)
        .catch(error => console.error("Error while sending message: " + error));
    } else {
      console.error("Hub connection is not established.");
    }
  }

  stopConnection(): void {
    if (this.hubConnection && this.hubConnection.state === HubConnectionState.Connected) {
      this.hubConnection.stop()
        .then(() => console.log("Hub connection stopped"))
        .catch(error => console.error("Error while stopping hub connection: " + error));
    } else {
      console.error("Hub connection is not established.");
    }
  }
}
