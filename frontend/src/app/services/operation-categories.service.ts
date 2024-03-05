import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { OperationCategory } from '../models/operation-category.model';

@Injectable({
  providedIn: 'root'
})
export class OperationCategoriesService {
  private backendUrl = 'https://localhost:44313/api/operations/categories';

  constructor(private http: HttpClient) { }

  getTypes(): Observable<OperationCategory[]> {
    const url = this.backendUrl;

    return this.http.get<OperationCategory[]>(url);
  }

  getTypeById(id: string): Observable<OperationCategory> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<OperationCategory>(url);
  }
}
