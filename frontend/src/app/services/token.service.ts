import { Injectable } from '@angular/core';

const TOKEN_KEY = 'auth_token';

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
}