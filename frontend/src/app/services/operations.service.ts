import { Injectable } from '@angular/core';
import { Operation } from '../models/operation.model';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { CreateOperation } from '../models/create-operation.model';

@Injectable({
  providedIn: 'root'
})
export class OperationsService {
  private backendUrl = 'https://localhost:44313/api/operations/operations';

  constructor(private http: HttpClient) { }

  getOperations(): Observable<Operation[]> {
    const url = this.backendUrl;

    return this.http.get<Operation[]>(url);
  }

  getOperationsByAccount(accountId: string): Observable<Operation[]> {
    const url = `${this.backendUrl}/account/${accountId}`;

    return this.http.get<Operation[]>(url);
  }

  getOperationById(id: string): Observable<Operation> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<Operation>(url);
  }

  addOperation(model: CreateOperation) {
    const url = `${this.backendUrl}`;

    return this.http.post(url, model);
  }
}
