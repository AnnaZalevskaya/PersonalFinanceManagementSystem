import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { User } from '../models/user.model';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class UsersService {
  private backendUrl = 'https://localhost:44313/api/auth/users';

  constructor(private http: HttpClient) { }

  getUsers(): Observable<User[]> {
    const url = `${this.backendUrl}/all-users`;

    return this.http.get<User[]>(url);
  }
}
