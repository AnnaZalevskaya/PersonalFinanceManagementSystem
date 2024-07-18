import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { IntervalEnum, RecurringOperationAction } from '../../../models/recurring-payment.model';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { NgxMaterialTimepickerModule, NgxMaterialTimepickerToggleComponent, NgxMaterialTimepickerComponent } from 'ngx-material-timepicker';
import { MatNativeDateModule } from '@angular/material/core';
import { MatButtonModule } from '@angular/material/button';
import { MatSelectModule } from '@angular/material/select';
import { ScheduleOperationsService } from '../../../services/schedule-recurring-operations.service';
import { CommonModule } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { Account } from '../../../models/account.model';
import { FinancialAccountsService } from '../../../services/financial-accounts.service';
import { PaginationSettings } from '../../../settings/pagination-settings';

@Component({
  selector: 'app-create-regular-payment',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatInputModule,
    MatDatepickerModule,
    NgxMaterialTimepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatButtonModule
  ],
  providers: [
    NgxMaterialTimepickerToggleComponent, 
    NgxMaterialTimepickerComponent
  ],
  templateUrl: './create-regular-payment.component.html',
  styleUrl: './create-regular-payment.component.css'
})
export class CreateRegularPaymentComponent implements OnInit {
  recurringOperationForm: FormGroup;

  intervals: { label: string, value: number }[];
  accounts: Account[] = [];

  userId: string;

  constructor(
    private recurringOperations: ScheduleOperationsService,
    private accountService: FinancialAccountsService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder
  ) {
    this.intervals = this.getIntervals();    
    this.userId = this.route.parent?.snapshot.paramMap.get('userId') ?? '';
    this.loadAccounts();

    this.recurringOperationForm = this.formBuilder.group({
      account: ['', Validators.required],
      name: ['', Validators.required],
      amount: ['', Validators.required],
      executionTime: ['', Validators.required],
      interval: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required]
    });
   }

  ngOnInit() {
    
  }

  loadAccounts() {
    this.accountService.getUserRecordsCount(this.userId).subscribe(
      response => {
        const paginationSettings = new PaginationSettings();
        paginationSettings.pageSize = response;
        this.accountService.getAccountsByUser(this.userId, paginationSettings).subscribe(
          accounts => {
            this.accounts = accounts; 
          },
          error => {
            console.error('Error retrieving accounts:', error);
          }
        );
      }
    )   
  }

  AddRegOper() {
    if (this.recurringOperationForm.valid) {
      const recPayment: RecurringOperationAction = {
        accountId: this.recurringOperationForm.get('account')!.value.id,
        userId: this.userId,
        name: this.recurringOperationForm.get('name')!.value,
        amount: this.recurringOperationForm.get('amount')!.value,
        startDate: this.recurringOperationForm.get('startDate')!.value,
        endDate: this.recurringOperationForm.get('endDate')!.value,
        interval: this.recurringOperationForm.get('interval')!.value.label,
        executionTime: this.recurringOperationForm.get('executionTime')!.value,
      };

      console.log("New rec pay " +
        recPayment.accountId + " | " + 
        recPayment.userId + " | " +
        recPayment.amount + " | " + 
        recPayment.startDate + " | " + 
        recPayment.endDate + " | " + 
        recPayment.interval + " | " + 
        recPayment.executionTime
       );
  
      this.recurringOperations.addOperation(recPayment).subscribe(
        response => {
          console.log("added");
        },
        error => {  
        }
      );
    }
  }

  getIntervals(): { label: string, value: IntervalEnum }[] {
    return Object.keys(IntervalEnum)
      .map((key: string) => ({
        label: key,
        value: IntervalEnum[key as keyof typeof IntervalEnum]
      }));
  }
}
