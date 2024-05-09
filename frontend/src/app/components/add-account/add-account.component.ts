import { Component, OnInit } from '@angular/core';
import { FinancialAccountsService } from '../../services/financial-accounts.service';
import { AccountTypesService } from '../../services/account-types.service';
import { CurrencyService } from '../../services/currency.service';
import { FormsModule } from '@angular/forms';
import { AccountType } from '../../models/account-type.model';
import { Currency } from '../../models/currency.model';
import { AccountAction } from '../../models/account-action.model';
import { CommonModule } from '@angular/common';
import { AuthService } from '../../services/auth.service';
import { SignalRService } from '../../services/signal-r.service';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';
import { Router } from '@angular/router';

@Component({
  selector: 'app-add-account',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    LoadingIndicatorComponent
  ],
  templateUrl: './add-account.component.html',
  styleUrl: './add-account.component.css'
})
export class AddAccountComponent implements OnInit {
  userId: number = 0;
  isLoadingForm: boolean = false;

  accountName: string = '';
  accountType!: AccountType;
  currency!: Currency;

  accountTypes: AccountType[] = [];
  currencies: Currency[] = [];

  constructor(
    private router: Router,
    private authService: AuthService,
    private accountsService: FinancialAccountsService,
    private typeService: AccountTypesService,
    private currencyService: CurrencyService,
    private signalRService: SignalRService
  ) {}

    ngOnInit(): void {
      this.signalRService.startConnection();
      
      this.loadAccountTypes();
      this.loadCurrencies();
      this.isLoadingForm = true;
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
      this.signalRService.startNotificationsListener();
      this.userId = this.authService.getCurrentUser()['id'];

      const newAccount: AccountAction = {
        name: this.accountName,
        accountTypeId: this.accountType.id,
        type: this.accountType,
        currencyId: this.currency.id,
        currency: this.currency,
        userId: this.userId,
      };

      console.log(newAccount);

      this.signalRService.sendNotification(this.userId.toString(), "New account has been added successfully");
  
    //  this.accountsService.addAccount(newAccount);

    this.router.navigate(['./']);
    }

    ngOnDestroy() {
      this.signalRService.stopConnection();
    }
}
