import { Injectable } from '@angular/core';
import { Subject } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  private reportSavedSubject = new Subject<string>();

  reportSaved$ = this.reportSavedSubject.asObservable();

  constructor() { }

  notifyReportSaved(reportName: string) {
    this.reportSavedSubject.next(reportName);
  }
}