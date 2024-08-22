import { Component, OnInit } from '@angular/core';
import { FinancialAccountsService } from '../../../services/financial-accounts.service';
import { AccountTypesService } from '../../../services/account-types.service';
import { CurrencyService } from '../../../services/currency.service';
import { AccountType } from '../../../models/account-type.model';
import { Currency } from '../../../models/currency.model';
import { AccountAction } from '../../../models/account-action.model';
import { CommonModule } from '@angular/common';
import {Validators, FormsModule, ReactiveFormsModule, FormBuilder, FormGroup } from '@angular/forms';
import {MatInputModule} from '@angular/material/input';
import {MatSelectModule} from '@angular/material/select';
import {MatFormFieldModule} from '@angular/material/form-field';
import { AuthService } from '../../../services/auth.service';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';
import { Router } from '@angular/router';
import { PaginationSettings } from '../../../settings/pagination-settings';

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

  accountForm: FormGroup;

  constructor(
    private router: Router,
    private authService: AuthService,
    private accountsService: FinancialAccountsService,
    private typeService: AccountTypesService,
    private currencyService: CurrencyService,
    private formBuilder: FormBuilder
  ) {
    this.accountForm = this.formBuilder.group({
      accountName: ['', Validators.required],
      accountType: ['', Validators.required],
      currency: ['', Validators.required]
    });
  }

  ngOnInit(): void {
    this.currPaginationSettings.pageSize = 8;
    this.typePaginationSettings.pageSize = 2;
     
    this.loadAccountTypes();
    this.loadCurrencies();
  }
  
  loadAccountTypes() {
    this.typeService.getTypes(this.typePaginationSettings).subscribe((types: AccountType[]) => {
      this.accountTypes = types;
    });
  }
  
  loadCurrencies() {
    this.currencyService.getCurrencies(this.currPaginationSettings).subscribe((currencies: Currency[]) => {
      this.currencies = currencies;
      this.isLoadingForm = true;
    });
  }

  AddAccount() {
    this.userId = this.authService.getCurrentUser()['id'];

    if (this.accountForm.valid) {
      const newAccount: AccountAction = {
        name: this.accountForm.get('accountName')!.value,   
        type: this.accountForm.get('accountType')!.value,
        accountTypeId: this.accountForm.get('accountType')!.value.id,  
        currency: this.accountForm.get('currency')!.value,
        currencyId: this.accountForm.get('currency')!.value.id,
        userId: this.userId,
      };
  
      this.accountsService.addAccount(newAccount).subscribe(
        response => {
          console.log("added");
        },
        error => {  
          this.router.navigate(['internal-server-error']);
          console.log("Something went wrong...");
        }
      );
  }

    this.router.navigate(['./profile/{{userId}}']);
  }
}
