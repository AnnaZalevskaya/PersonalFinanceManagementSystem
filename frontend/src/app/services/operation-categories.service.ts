import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { HttpClient, HttpParams } from '@angular/common/http';
import { OperationCategory } from '../models/operation-category.model';
import { PaginationSettings } from '../settings/pagination-settings';

@Injectable({
  providedIn: 'root'
})
export class OperationCategoriesService {
  private backendUrl = 'https://localhost:44313/api/operations/categories';

  constructor(private http: HttpClient) { }

  getCategories(paginationSettings: PaginationSettings): Observable<OperationCategory[]> {
    const url = this.backendUrl;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<OperationCategory[]>(url, { params });
  }

  getCategoryById(id: string): Observable<OperationCategory> {
    const url = `${this.backendUrl}/${id}`;

    return this.http.get<OperationCategory>(url);
  }
}
