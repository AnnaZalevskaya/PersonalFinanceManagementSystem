import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { AccountType } from '../models/account-type.model';

@Injectable({
  providedIn: 'root'
})
export class AccountTypesService {
  private backendUrl = 'https://localhost:44313/api/accounts/financial-account-types';

  constructor(private http: HttpClient) { }

  getTypes(): Observable<AccountType[]> {
    const url = this.backendUrl;

    return this.http.get<AccountType[]>(url);
  }

  getTypeById(id: string): Observable<AccountType> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<AccountType>(url);
  }
}
