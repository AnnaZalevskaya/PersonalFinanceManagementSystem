import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, tap } from 'rxjs';
import { AuthRequest, AuthResponse } from '../models/auth.model';
import { Token } from '../models/token.model';
import { RegisterRequest, RegisterResponse } from '../models/register.model';
import { TokenService } from './token.service';

const USER_KEY = 'auth_user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  userId!: string | null;
  tokens!: Token;

  private backendUrl = "https://localhost:44313/api/auth/auth";

  constructor(private http: HttpClient, private tokenService: TokenService) 
  {
    
  }

  authenticate(model: AuthRequest): Observable<AuthResponse> {
    const url = `${this.backendUrl}/authenticate`;

    return this.http.post<AuthResponse>(url, model);
  }

  register(model : RegisterRequest): Observable<RegisterResponse> {
    const url = `${this.backendUrl}/register`

    return this.http.post<RegisterResponse>(url, model);
  }

  refreshAccessToken(): Observable<Token> {
    const tokenModel: Token = {
      accessToken: this.getAccessToken()!,
      refreshToken: this.getRefreshToken()!
    };

    const url = `${this.backendUrl}/refresh-token`;

    return this.http.post<Token>(url, tokenModel)
    .pipe(
      tap((response) => {
        const newAccessToken = response.accessToken;
        const newRefreshToken = response.refreshToken;

        this.saveAccessToken(newAccessToken!);
        this.saveRefreshToken(newRefreshToken!);
      })
    );;
  }

  public getCurrentUser(): any {
    const user = localStorage.getItem(USER_KEY);
    if (user) {
      return JSON.parse(user);
    }

    return {};
  }

  isLoggedIn(): boolean {
    const token = this.tokenService.getToken();

    return !!token;   
  }

  getAccessToken() {
    return this.tokenService.getToken();
  }

  getRefreshToken() {
    return this.tokenService.getRefreshToken();
  }

  saveAccessToken(token: string) {
    this.tokenService.saveToken(token);
  }

  saveRefreshToken(token: string) {
    this.tokenService.saveRefreshToken(token);
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