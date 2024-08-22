import { Injectable } from '@angular/core';
import { Operation } from '../models/operation.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { PaginationSettings } from '../settings/pagination-settings';
import { RecurringOperation, RecurringOperationAction } from '../models/recurring-payment.model';

@Injectable({
  providedIn: 'root'
})
export class ScheduleOperationsService {
  private backendUrl = 'https://localhost:44313/api/operations/schedule-recurring-operations';

  constructor(private http: HttpClient) { }

  getOperationsByUser(userId: string, paginationSettings: PaginationSettings): Observable<RecurringOperation[]> {
    const url = `${this.backendUrl}/user/${userId}`;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<RecurringOperation[]>(url, { params });
  }

  getUserRecordsCount(userId: string): Observable<number> {
    const url = `${this.backendUrl}/count_for_user/${userId}`;

    return this.http.get<number>(url);
  }

  getOperationById(id: string): Observable<RecurringOperation> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<RecurringOperation>(url);
  }

  addOperation(model: RecurringOperationAction) {
    const url = this.backendUrl;

    return this.http.post(url, model);
  }

  updateOperation(id: string, model: RecurringOperationAction) {
    const url = `${this.backendUrl}/${id}`;

    return this.http.post(url, model);
  }

  deleteAccountOperations(accountId: number): Observable<any> {
    const url = `${this.backendUrl}/account/${accountId}`;
    
    return this.http.delete(url);
  }

  deleteOperation(id: string) {
    const url = `${this.backendUrl}/${id}`;
    
    return this.http.delete(url);
  }
}
