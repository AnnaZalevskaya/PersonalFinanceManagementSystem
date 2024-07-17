import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { FinancialAccountsService } from '../../services/financial-accounts.service';
import { AccountTypesService } from '../../services/account-types.service';
import { CurrencyService } from '../../services/currency.service';
import { Currency } from '../../models/currency.model';
import { AccountType } from '../../models/account-type.model';
import { AuthService } from '../../services/auth.service';
import { AccountAction } from '../../models/account-action.model';
import { PaginationSettings } from '../../settings/pagination-settings';

@Component({
  selector: 'app-update-account',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule,
    RouterModule
  ],
  templateUrl: './update-account.component.html',
  styleUrl: './update-account.component.css'
})
export class UpdateAccountComponent implements OnInit {
  accountTypes: AccountType[] = [];
  currencies: Currency[] = [];

  accountName: string = '';
  accountType!: AccountType;
  currency!: Currency;

  userId: number = 0;
  accountId!: string;

  currPaginationSettings: PaginationSettings = new PaginationSettings();
  typePaginationSettings: PaginationSettings = new PaginationSettings();

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private accountService: FinancialAccountsService,
    private typeService: AccountTypesService,
    private currencyService: CurrencyService
  ) {}

  ngOnInit(): void {
    const id = this.route.parent?.snapshot.paramMap.get('id');
    if (id) {
      this.accountId = id;
    }
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
    });
  }

  editAccount() {
    this.userId = this.authService.getCurrentUser()['id'];

      const updateAccount: AccountAction = {
        name: this.accountName,
        accountTypeId: this.accountType.id,
        type: this.accountType,
        currencyId: this.currency.id,
        currency: this.currency,
        userId: this.userId,
      };

    this.accountService.updateAccount(this.userId.toString(), this.accountId.toString(), updateAccount)
  }
}
