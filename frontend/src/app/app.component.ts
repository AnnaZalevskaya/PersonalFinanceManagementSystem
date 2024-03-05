import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./components/header/header.component";
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { FinancialAccountsService } from './services/financial-accounts.service';
import { AuthService } from './services/auth.service';
import { OperationsService } from './services/operations.service';
import { AccountTypesService } from './services/account-types.service';
import { CurrencyService } from './services/currency.service';
import { TokenService } from './services/token.service';
import { UsersService } from './services/users.service';
import { OperationCategoriesService } from './services/operation-categories.service';
import { AuthInterceptor } from './extensions/auth.interceptor';
import { AuthGuard } from './extensions/auth.guard';
import { NgxPaginationModule } from 'ngx-pagination';
import { SignalRService } from './services/signal-r.service';
import { UserNotificationsService } from './services/user-notifications.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet, 
    HeaderComponent, 
    HttpClientModule,
    NgxPaginationModule
  ],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css', 
  providers: [
    AuthService,
    TokenService,
    UsersService,
    FinancialAccountsService,
    AccountTypesService,
    CurrencyService,
    OperationsService, 
    OperationCategoriesService,
    SignalRService,
    UserNotificationsService,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    },
    AuthGuard
  ]
})
export class AppComponent {
  title = 'frontend';
}
