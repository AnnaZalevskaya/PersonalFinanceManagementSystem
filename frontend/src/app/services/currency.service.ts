import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Currency } from '../models/currency.model';
import { PaginationSettings } from '../settings/pagination-settings';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {
  private backendUrl = 'https://localhost:44313/api/accounts/currencies';

  constructor(private http: HttpClient) { }

  getCurrencies(paginationSettings: PaginationSettings): Observable<Currency[]> {
    const url = this.backendUrl;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<Currency[]>(url, { params });
  }

  getCurrencyById(id: string): Observable<Currency> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<Currency>(url);
  }
}
