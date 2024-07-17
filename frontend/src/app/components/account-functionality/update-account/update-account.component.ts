import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FinancialAccountsService } from '../../../services/financial-accounts.service';
import { AccountTypesService } from '../../../services/account-types.service';
import { CurrencyService } from '../../../services/currency.service';
import { Currency } from '../../../models/currency.model';
import { AccountType } from '../../../models/account-type.model';
import { AuthService } from '../../../services/auth.service';
import { AccountAction } from '../../../models/account-action.model';
import { PaginationSettings } from '../../../settings/pagination-settings';
import { MatSelectModule } from '@angular/material/select';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';

@Component({
  selector: 'app-update-account',
  standalone: true,
  imports: [
    CommonModule, 
    MatFormFieldModule, 
    MatSelectModule, 
    FormsModule, 
    ReactiveFormsModule, 
    MatInputModule,
    RouterModule,
    LoadingIndicatorComponent
  ],
  templateUrl: './update-account.component.html',
  styleUrl: './update-account.component.css'
})
export class UpdateAccountComponent implements OnInit {
  isLoadingForm: boolean = false;

  accountTypes: AccountType[] = [];
  currencies: Currency[] = [];

  userId: number = 0;
  accountId!: string;

  currPaginationSettings: PaginationSettings = new PaginationSettings();
  typePaginationSettings: PaginationSettings = new PaginationSettings();

  accountForm: FormGroup;
  selectedType!: AccountType;

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private authService: AuthService,
    private accountService: FinancialAccountsService,
    private typeService: AccountTypesService,
    private currencyService: CurrencyService,
    private formBuilder: FormBuilder
  ) {
    const id = this.route.parent?.snapshot.paramMap.get('id');
    console.log("id " + id);

    if (id) {
      this.accountId = id;
      this.selectData();
    } 

    this.accountForm = this.formBuilder.group({
      accountName: ['', Validators.required],
      accountType: ['', Validators.required],
      currency: ['', Validators.required]
    });
  }

  ngOnInit(): void {
  
  }

  selectData() {
    this.accountService.getAccountById(this.accountId).subscribe(
      account => {
        this.typeService.getTypes(this.typePaginationSettings).subscribe((types: AccountType[]) => {
          this.accountTypes = types;

          this.currencyService.getCurrencies(this.currPaginationSettings).subscribe((currencies: Currency[]) => {
            this.currencies = currencies;

            const selectedCurrency = this.currencies.find(curr => curr.name === account.currency.name);
            const selectedAccountType = this.accountTypes.find(type => type.name === account.accountType.name);

            this.accountForm.patchValue({
              accountName: account.name,
              accountType: selectedAccountType,
              currency: selectedCurrency
            });
            
            this.isLoadingForm = true;
          });
        });
      }
    );
  } 

  editAccount() {
    this.userId = this.authService.getCurrentUser()['id'];

    console.log(this.userId)
    if (this.accountForm.valid) {
      const updateAccount: AccountAction = {
        name: this.accountForm.get('accountName')!.value,   
        type: this.accountForm.get('accountType')!.value,
        accountTypeId: this.accountForm.get('accountType')!.value.id,  
        currency: this.accountForm.get('currency')!.value,
        currencyId: this.accountForm.get('currency')!.value.id,
        userId: this.userId,
      };
    
      this.accountService.updateAccount(this.userId.toString(), this.accountId.toString(), updateAccount).subscribe(
        response => {
          this.router.navigate(['./profile/{{userId}}']);
          console.log("updated");
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
