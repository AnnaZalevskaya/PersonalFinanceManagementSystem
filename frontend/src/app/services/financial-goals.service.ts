import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { PaginationSettings } from '../settings/pagination-settings';
import { ActionGoal, Goal } from '../models/goal.model';

@Injectable({
  providedIn: 'root'
})
export class FinancialGoalsService {
  private backendUrl = 'https://localhost:44313/api/accounts/financial-goals';

  constructor(private http: HttpClient) { }

  getGoals(paginationSettings: PaginationSettings): Observable<Goal[]> {
    const url = this.backendUrl;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<Goal[]>(url, { params });
  }

  getRecordsCount(): Observable<number> {
    const url = `${this.backendUrl}/count`;

    return this.http.get<number>(url);
  }

  getGoalsByAccount(accountId: string, paginationSettings: PaginationSettings): Observable<Goal[]> {
    const url = `${this.backendUrl}/account/${accountId}`;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<Goal[]>(url, { params });
  }

  getAccountRecordsCount(accountId: string): Observable<number> {
    const url = `${this.backendUrl}/count_for_account/${accountId}`;

    return this.http.get<number>(url);
  }

  getGoalById(id: string): Observable<Goal> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<Goal>(url);
  }

  addGoal(account: ActionGoal) {
    const url = this.backendUrl;

    return this.http.post(url, account);
  }

  deleteGoal(userId: string, accountId: string): Observable<any> {
    const url = `${this.backendUrl}/user/${userId}/goal/${accountId}`;
    
    return this.http.delete(url);
  }

  updateGoal(accountId: string, id: string, model: ActionGoal): Observable<any> {
    const url = `${this.backendUrl}/account/${accountId}/goal/${id}`;

    return this.http.put(url, model);
  }
}