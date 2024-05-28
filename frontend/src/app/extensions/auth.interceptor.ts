import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from '@angular/common/http';
import { Observable, catchError, switchMap, throwError } from 'rxjs';
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

        return next.handle(authRequest).pipe(
          catchError((error) => {
            if (error.status === 401) {
              return this.refreshTokenAndRetry(request, next);
            }
  
            return throwError(error);
          })
        );
    } 
    
    return next.handle(request);
  }

  private refreshTokenAndRetry(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return this.authService.refreshAccessToken().pipe(
      switchMap(() => {
        const newAuthToken = this.authService.getAccessToken();

        if (newAuthToken) {
          const authRequest = request.clone({
            headers: request.headers.set('Authorization', `Bearer ${newAuthToken}`)
          });

          return next.handle(authRequest);
        }

        return throwError('Failed to refresh access token.');
      })
    );
  }
}