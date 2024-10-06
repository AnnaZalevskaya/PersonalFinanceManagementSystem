import { Injectable } from '@angular/core';
import * as signalr from '@aspnet/signalr';
import { Subject } from 'rxjs';
import { GoalAchievedNotification } from '../models/user-notification.model';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  private hubConnection!: signalr.HubConnection;
  public messageReceived = new Subject<{ user: string, message: string }>();
  private goalAchievedNotificationSubject: Subject<GoalAchievedNotification> = new Subject<GoalAchievedNotification>();

  constructor() {
   // this.startConnection();
  }

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

  private registerGoalAchievedNotificationHandler(): void {
    this.hubConnection.on('ReceiveGoalAchievedNotification', (notification: GoalAchievedNotification) => {
      this.goalAchievedNotificationSubject.next(notification);
    });
  }

  public sendGoalAchievedNotification(userId: string, goalName: string): void {
    this.hubConnection.invoke('SendGoalAchievedNotification', userId, goalName)
      .catch(err => console.error('Error while sending goal achieved notification:', err));
  }

  public getGoalAchievedNotificationStream(): Subject<GoalAchievedNotification> {
    return this.goalAchievedNotificationSubject;
  }
}