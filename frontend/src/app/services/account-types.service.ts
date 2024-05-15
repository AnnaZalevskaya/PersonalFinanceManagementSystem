import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable, map } from 'rxjs';
import { AccountType } from '../models/account-type.model';
import { PaginationSettings } from '../settings/pagination-settings';

@Injectable({
  providedIn: 'root'
})
export class AccountTypesService {
  private backendUrl = 'https://localhost:44313/api/accounts/financial-account-types';

  constructor(private http: HttpClient) { }

  getTypes(paginationSettings: PaginationSettings): Observable<AccountType[]> {
    const url = this.backendUrl;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<AccountType[]>(url, { params });
  }

  getTypeById(id: string): Observable<AccountType> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<AccountType>(url);
  }
}
