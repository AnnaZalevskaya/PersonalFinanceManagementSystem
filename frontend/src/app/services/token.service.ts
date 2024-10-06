import { Injectable } from '@angular/core';

const TOKEN_KEY = 'auth_token';
const REFRESH_KEY = 'refresh_token';

@Injectable({
  providedIn: 'root'
})
export class TokenService {
  
  constructor() { }

  saveToken(token: string) {
    if (token){
      localStorage.setItem(TOKEN_KEY, token);
    }   
  }
  
  getToken() {
    return localStorage.getItem(TOKEN_KEY);
  }
  
  removeToken() {
    localStorage.removeItem(TOKEN_KEY);
  }

  saveRefreshToken(refreshToken: string) {
    if (refreshToken){
      localStorage.setItem(REFRESH_KEY, refreshToken);
    }   
  }
  
  getRefreshToken() {
    return localStorage.getItem(REFRESH_KEY);
  }
  
  removeRefreshToken() {
    localStorage.removeItem(REFRESH_KEY);
  }
}