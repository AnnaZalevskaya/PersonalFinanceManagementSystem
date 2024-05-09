import { Injectable } from '@angular/core';
import * as signalr from '@aspnet/signalr';
import { UserNotification } from '../models/user-notification.model';
import { UserNotificationsService } from './user-notifications.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalr.HubConnection;

  constructor(private notificationsService: UserNotificationsService) {}

  startConnection(): void {
    this.hubConnection = new signalr.HubConnectionBuilder()
      .withUrl('https://localhost:44304/notifications', {
        skipNegotiation: true,
        transport: signalr.HttpTransportType.WebSockets
      })
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.error('Error while starting connection: ' + err));
  }

  startNotificationsListener(): void {
    this.hubConnection.on("ReceiveNotification", message => {
      const notification: UserNotification = { message: message, isRead: false };
      this.notificationsService.addNotification(notification);
    });
  }

  sendNotification(userId: string, message: string): void {
    this.hubConnection.invoke("sendNotification", userId, message)
      .catch(error => console.error("Error while sending message: " + error));
  } 

  stopConnection(): void {
    this.hubConnection.stop()
      .then(() => console.log("Hub connection stopped"))
      .catch(error => console.error("Error while stopping hub connection: " + error));
  }
}