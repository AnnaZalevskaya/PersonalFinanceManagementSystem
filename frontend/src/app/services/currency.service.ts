import { Injectable } from '@angular/core';
import { Observable, map } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { Currency } from '../models/currency.model';

@Injectable({
  providedIn: 'root'
})
export class CurrencyService {
  private backendUrl = 'https://localhost:44313/api/accounts/currencies';

  constructor(private http: HttpClient) { }

  getCurrencies(): Observable<Currency[]> {
    const url = this.backendUrl;

    return this.http.get<Currency[]>(url);
  }

  getCurrencyById(id: string): Observable<Currency> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<Currency>(url);
  }
}
