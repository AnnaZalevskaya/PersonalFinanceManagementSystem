import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthRequest, AuthResponse } from '../models/auth.model';
import { RegisterRequest, RegisterResponse } from '../models/register.model';
import { TokenService } from './token.service';

const USER_KEY = 'auth_user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userId!: string | null;

  private backendUrl = "https://localhost:44313/api/auth/users";

  constructor(private http: HttpClient, private tokenService: TokenService) {}

  authenticate(model: AuthRequest): Observable<AuthResponse> {
    const url = `${this.backendUrl}/authenticate`;

    return this.http.post<AuthResponse>(url, model);
  }

  register(model : RegisterRequest): Observable<RegisterResponse> {
    const url = `${this.backendUrl}/register`

    return this.http.post<RegisterResponse>(url, model);
  }

  public getCurrentUser(): any {
    const user = localStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }
    console.log("model " + {})

    return {};
  }

  isLoggedIn(): boolean {
    const token = this.tokenService.getToken();
    console.log("token " + token)

    return !!token;   
  }

  getAccessToken() {
    return this.tokenService.getToken();
  }

  getRefreshToken() {
    return this.getCurrentUser()['refreshToken'];
  }

  saveAccessToken(token: string) {
    this.tokenService.saveToken(token);
  }

  logOut() {
    this.tokenService.removeToken();
    this.deleteUser();
  }

  public saveUser(user: any): void {
    localStorage.setItem(USER_KEY, JSON.stringify(user));
  }

  public deleteUser(): void {
    localStorage.removeItem(USER_KEY);
  }
}