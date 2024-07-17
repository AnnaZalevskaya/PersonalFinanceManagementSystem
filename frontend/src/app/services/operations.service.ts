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

<<<<<<< HEAD
  getRecordsCount(): Observable<number> {
    const url = `${this.backendUrl}/count`;

    return this.http.get<number>(url);
  }

=======
>>>>>>> 4510382431133ede313c8245fd273105d98638c9
  getOperationsByAccount(accountId: string, paginationSettings: PaginationSettings): Observable<Operation[]> {
    const url = `${this.backendUrl}/account/${accountId}`;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<Operation[]>(url, { params });
<<<<<<< HEAD
  }

  getAccountRecordsCount(accountId: string): Observable<number> {
    const url = `${this.backendUrl}/count_for_account/${accountId}`;

    return this.http.get<number>(url);
=======
>>>>>>> 4510382431133ede313c8245fd273105d98638c9
  }

  getOperationById(id: string): Observable<Operation> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<Operation>(url);
  }

  addOperation(model: CreateOperation) {
    const url = this.backendUrl;

    return this.http.post(url, model);
  }

<<<<<<< HEAD
  deleteAccountOperations(accountId: string): Observable<any> {
=======
  deleteAccountOperations(accountId: number): Observable<any> {
>>>>>>> 4510382431133ede313c8245fd273105d98638c9
    const url = `${this.backendUrl}/account/${accountId}`;
    
    return this.http.delete(url);
  }
}
