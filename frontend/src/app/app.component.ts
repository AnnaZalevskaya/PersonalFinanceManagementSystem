import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HeaderComponent } from "./components/header/header.component";
import { HttpClientModule } from '@angular/common/http';
import { FinancialAccountsService } from './services/financial-accounts.service';
import { AuthService } from './services/auth.service';
import { OperationsService } from './services/operations.service';
import { AccountTypesService } from './services/account-types.service';
import { CurrencyService } from './services/currency.service';
import { TokenService } from './services/token.service';
import { UsersService } from './services/users.service';
import { OperationCategoriesService } from './services/operation-categories.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [
    RouterOutlet, 
    HeaderComponent, 
    HttpClientModule
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
    OperationCategoriesService
  ]
})
export class AppComponent {
  title = 'frontend';
}
