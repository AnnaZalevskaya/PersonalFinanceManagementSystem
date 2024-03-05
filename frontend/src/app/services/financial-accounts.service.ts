import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { Account } from '../models/account.model';
import { AccountAction } from '../models/account-action.model';
import { CurrencyService } from './currency.service';
import { AccountTypesService } from './account-types.service';

@Injectable({
  providedIn: 'root'
})
export class FinancialAccountsService {
  private backendUrl = 'https://localhost:44313/api/accounts/financial-accounts';

  constructor(
    private http: HttpClient,
    private typeServive: AccountTypesService, 
    private currencyService: CurrencyService) { }

  getAccounts(): Observable<Account[]> {
    const url = this.backendUrl;

    return this.http.get<Account[]>(url);
  }

  getAccountsByUser(userId: string): Observable<Account[]> {
    const url = `${this.backendUrl}/user/${userId}`;

    return this.http.get<Account[]>(url);
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
    const url = `${this.backendUrl}/${userId}/${accountId}`;
    
    return this.http.delete(url);
  }

  updateAccount(accountId: string, model: AccountAction): Observable<any> {
    const url = `${this.backendUrl}/${accountId}`;

    return this.http.put(url, model);
  }
}
