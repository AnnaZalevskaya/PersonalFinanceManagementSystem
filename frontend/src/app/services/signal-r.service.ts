import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { UserNotification } from '../models/user-notification.model';
import { UserNotificationsService } from './user-notifications.service';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalR.HubConnection;

  constructor(private notificationsService: UserNotificationsService) {}

  startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl('https://localhost:44304/notifications', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets
      }) 
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  startNotificationsListener() {
    this.hubConnection.on('ReceiveNotification', message => {
      const notification: UserNotification = { message: message, isRead: false };
      this.notificationsService.addNotification(notification);
    });
  }

  sendNotification(userId: string, message: string) {
    this.hubConnection.invoke("SendNotificationToUser", userId, message)
      .catch(error => console.log("Error while sending message: " + error));
  }

  stopConnection() {
    this.hubConnection.stop().then(() => {
      console.log("Hub connection stopped")
    })
      .catch(error => console.log("Error while stopping hub connection: " + error))
  }
}
