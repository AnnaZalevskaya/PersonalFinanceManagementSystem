import { Component, OnInit } from '@angular/core';
import { FinancialAccountsService } from '../../services/financial-accounts.service';
import { AccountTypesService } from '../../services/account-types.service';
import { CurrencyService } from '../../services/currency.service';
import { AccountType } from '../../models/account-type.model';
import { Currency } from '../../models/currency.model';
import { AccountAction } from '../../models/account-action.model';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroupDirective, NgForm, Validators, FormsModule, ReactiveFormsModule } from '@angular/forms';
import {ErrorStateMatcher} from '@angular/material/core';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { AuthService } from '../../services/auth.service';
import { SignalRService } from '../../services/signal-r.service';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';
import { Router } from '@angular/router';
import { PaginationSettings } from '../../settings/pagination-settings';

export class MyErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean {
    const isSubmitted = form && form.submitted;
    return !!(control && control.invalid && (control.dirty || control.touched || isSubmitted));
  }
}

@Component({
  selector: 'app-add-account',
  standalone: true,
  imports: [
    MatFormFieldModule, 
    MatSelectModule, 
    FormsModule, 
    ReactiveFormsModule, 
    MatInputModule,
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

  messages: { user: string, message: string }[] = [];

  currPaginationSettings: PaginationSettings = new PaginationSettings();
  typePaginationSettings: PaginationSettings = new PaginationSettings();

  selected = new FormControl('valid', [Validators.required, Validators.pattern('valid')]);

  selectFormControl = new FormControl('valid', [Validators.required, Validators.pattern('valid')]);

  nativeSelectFormControl = new FormControl('valid', [
    Validators.required,
    Validators.pattern('valid'),
  ]);

  matcher = new MyErrorStateMatcher();

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
    this.signalRService.startNotificationsListener();
     
    this.loadAccountTypes();
    this.loadCurrencies();

    this.isLoadingForm = true;

    this.signalRService.messageReceived.subscribe(msg => {
      this.messages.push(msg);
    });
  }
  
  loadAccountTypes() {
    this.typeService.getTypes(this.typePaginationSettings).subscribe((types: AccountType[]) => {
      this.accountTypes = types;
    });
  }
  
  loadCurrencies() {
    this.currencyService.getCurrencies(this.currPaginationSettings).subscribe((currencies: Currency[]) => {
      this.currencies = currencies;
    });
  }

  AddAccount() {
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
