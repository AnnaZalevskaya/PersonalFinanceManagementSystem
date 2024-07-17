import { HttpClient, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';
import { PaginationSettings } from '../settings/pagination-settings';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private backendUrl = 'https://localhost:44313/api/auth/users';

  constructor(private http: HttpClient) { }

  getUsers(paginationSettings: PaginationSettings): Observable<User[]> {
    const url = `${this.backendUrl}/all-users`;

    const params = new HttpParams()
      .set('pageNumber', paginationSettings.pageNumber.toString())
      .set('pageSize', paginationSettings.pageSize.toString());

    return this.http.get<User[]>(url, { params });
  }

  getUserById(id: string): Observable<User> {
    const url = `${this.backendUrl}/user-info/${id}`;

    return this.http.get<User>(url);
<<<<<<< HEAD
  }

  getRecordsCount(): Observable<number> {
    const url = `${this.backendUrl}/count`;

    return this.http.get<number>(url);
=======
>>>>>>> 4510382431133ede313c8245fd273105d98638c9
  }
}
