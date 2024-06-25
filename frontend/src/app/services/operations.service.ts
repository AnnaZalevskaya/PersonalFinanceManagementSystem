import { Injectable } from '@angular/core';
import { Operation } from '../models/operation.model';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { CreateOperation } from '../models/create-operation.model';
import { PaginationSettings } from '../settings/pagination-settings';

@Injectable({
  providedIn: 'root'
})
export class OperationsService {
  private backendUrl = 'https://localhost:44313/api/operations/operations';

  constructor(private http: HttpClient) { }

  getOperations(paginationSettings: PaginationSettings): Observable<Operation[]> {
    const url = this.backendUrl;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<Operation[]>(url, { params });
  }

  getRecordsCount(): Observable<number> {
    const url = `${this.backendUrl}/count`;

    return this.http.get<number>(url);
  }

  getOperationsByAccount(accountId: string, paginationSettings: PaginationSettings): Observable<Operation[]> {
    const url = `${this.backendUrl}/account/${accountId}`;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<Operation[]>(url, { params });
  }

  getAccountRecordsCount(accountId: string): Observable<number> {
    const url = `${this.backendUrl}/count_for_account/${accountId}`;

    return this.http.get<number>(url);
  }

  getOperationById(id: string): Observable<Operation> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<Operation>(url);
  }

  addOperation(model: CreateOperation) {
    const url = this.backendUrl;

    return this.http.post(url, model);
  }

  deleteAccountOperations(accountId: string): Observable<any> {
    const url = `${this.backendUrl}/account/${accountId}`;
    
    return this.http.delete(url);
  }
}
