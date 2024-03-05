import { Component } from '@angular/core';
import { FinancialAccountsService } from '../../services/financial-accounts.service';
import { AccountTypesService } from '../../services/account-types.service';
import { CurrencyService } from '../../services/currency.service';
import { FormsModule } from '@angular/forms';
import { AccountType } from '../../models/account-type.model';
import { Currency } from '../../models/currency.model';
import { AccountAction } from '../../models/account-action.model';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { AuthGuard } from '../../extensions/auth.guard';
import { SignalRService } from '../../services/signal-r.service';

@Component({
  selector: 'app-add-account',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule
  ],
  templateUrl: './add-account.component.html',
  styleUrl: './add-account.component.css'
})
export class AddAccountComponent {
  userId: number = 0;

  accountName: string = '';
  accountType!: AccountType;
  currency!: Currency;

  accountTypes: AccountType[] = [];
  currencies: Currency[] = [];

  constructor(
    private authService: AuthService,
    private accountsService: FinancialAccountsService,
    private typeService: AccountTypesService,
    private currencyService: CurrencyService,
    private signalRService: SignalRService
  ) {}

    ngOnInit() {
      this.signalRService.startConnection();
      this.signalRService.startNotificationsListener();
      this.loadAccountTypes();
      this.loadCurrencies();
    }
  
    loadAccountTypes() {
      this.typeService.getTypes().subscribe((types: AccountType[]) => {
        this.accountTypes = types;
      });
    }
  
    loadCurrencies() {
      this.currencyService.getCurrencies().subscribe((currencies: Currency[]) => {
        this.currencies = currencies;
      });
    }

    Add() {
      this.userId = this.authService.getCurrentUser()['id'];

      const newAccount: AccountAction = {
        name: this.accountName,
        accountTypeId: this.accountType.id,
        type: this.accountType,
        currencyId: this.currency.id,
        currency: this.currency,
        userId: this.userId,
      };
      this.signalRService.sendNotification(this.userId.toString(), "New account has been added successfully");

      console.log(newAccount)
  
      this.accountsService.addAccount(newAccount);
    }

    ngOnDestroy() {
      this.signalRService.stopConnection();
    }
}
