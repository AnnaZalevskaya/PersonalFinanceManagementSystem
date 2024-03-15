import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable } from 'rxjs';
import { AuthService } from '../services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthInterceptor implements HttpInterceptor {
    constructor(private authService: AuthService) {}

    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

    const authToken = this.authService.getAccessToken();

    if (authToken) {
        const authRequest = request.clone({
        headers: request.headers.set('Authorization', `Bearer ${authToken}`)
        });

        return next.handle(authRequest);
    } 
    
    return next.handle(request);
  }
}