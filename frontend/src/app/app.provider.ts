import { Provider } from '@angular/core';

import { HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthInterceptor } from './extensions/auth.interceptor';
import { AuthGuard } from './extensions/auth.guard';

export const InterceptorProvider: Provider = [
  AuthGuard,
  { 
    provide: HTTP_INTERCEPTORS, 
    useClass: AuthInterceptor, 
    multi: true 
  }
];