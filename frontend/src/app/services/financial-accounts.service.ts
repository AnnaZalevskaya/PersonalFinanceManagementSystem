import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Account } from '../models/account.model';
import { AccountAction } from '../models/account-action.model';
import { PaginationSettings } from '../settings/pagination-settings';

@Injectable({
  providedIn: 'root'
})
export class FinancialAccountsService {
  private backendUrl = 'https://localhost:44313/api/accounts/financial-accounts';

  constructor(private http: HttpClient) { }

  getAccounts(paginationSettings: PaginationSettings): Observable<Account[]> {
    const url = this.backendUrl;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<Account[]>(url, { params });
  }

  getRecordsCount(): Observable<number> {
    const url = `${this.backendUrl}/count`;

    return this.http.get<number>(url);
  }

  getAccountsByUser(userId: string, paginationSettings: PaginationSettings): Observable<Account[]> {
    const url = `${this.backendUrl}/user/${userId}`;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<Account[]>(url, { params });
  }

  getUserRecordsCount(userId: string): Observable<number> {
    const url = `${this.backendUrl}/count_for_user/${userId}`;

    return this.http.get<number>(url);
  }

  getAccountById(id: string): Observable<Account> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<Account>(url);
  }

  addAccount(account: AccountAction) {
    const url = `${this.backendUrl}`;

    return this.http.post(url, account);
  }

  deleteAccount(userId: string, accountId: string): Observable<any> {
    const url = `${this.backendUrl}/user/${userId}/account/${accountId}`;
    
    return this.http.delete(url);
  }

  updateAccount(userId: string, accountId: string, model: AccountAction): Observable<any> {
    const url = `${this.backendUrl}/user/${userId}/account/${accountId}`;

    return this.http.put(url, model);
  }
}
