import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatButtonModule } from '@angular/material/button';
import { MatNativeDateModule } from '@angular/material/core';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { ScheduleOperationsService } from '../../../services/schedule-recurring-operations.service';
import { IntervalEnum, RecurringOperationAction } from '../../../models/recurring-payment.model';
import { ActivatedRoute, Router } from '@angular/router';
import { Account } from '../../../models/account.model';
import { FinancialAccountsService } from '../../../services/financial-accounts.service';
import { PaginationSettings } from '../../../settings/pagination-settings';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';

@Component({
  selector: 'app-update-regular-payment',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatInputModule,
    MatDatepickerModule,
    MatNativeDateModule,
    MatSelectModule,
    MatButtonModule,
    LoadingIndicatorComponent
  ],
  templateUrl: './update-regular-payment.component.html',
  styleUrl: './update-regular-payment.component.css'
})
export class UpdateRegularPaymentComponent {
  isLoadingForm: boolean = false;

  recurringOperationForm: FormGroup;

  intervals: { label: string, value: number }[];
  accounts: Account[] = [];

  userId: string;
  regOperId: string;

  constructor(
    private recurringOperationsService: ScheduleOperationsService,
    private accountService: FinancialAccountsService,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder
  ) {
    this.intervals = this.getIntervals();    
    this.userId = this.route.parent?.snapshot.paramMap.get('userId') ?? '';
    this.regOperId = this.route.snapshot.paramMap.get('regPayId') ?? '';

    this.recurringOperationForm = this.formBuilder.group({
      account: ['', Validators.required],
      name: ['', Validators.required],
      amount: ['', Validators.required],
      executionTime: ['', Validators.required],
      interval: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required]
    });

    this.selectData();
   }

  ngOnInit() {
    
  }

  getIntervals(): { label: string, value: IntervalEnum }[] {
    return Object.keys(IntervalEnum)
      .map((key: string) => ({
        label: key,
        value: IntervalEnum[key as keyof typeof IntervalEnum]
      }));
  }

  selectData() {
    this.accountService.getUserRecordsCount(this.userId).subscribe(
      response => {
        const paginationSettings = new PaginationSettings();
        paginationSettings.pageSize = response;
        this.accountService.getAccountsByUser(this.userId, paginationSettings).subscribe(
          accounts => {
            this.accounts = accounts; 

            this.recurringOperationsService.getOperationById(this.regOperId).subscribe(
              operation => {
                const selectedIntervalForReccuring = this.intervals.find(interval => interval.label === operation.intervalType.toString());
                const selectedAccount = this.accounts.find(account => account.name === account.name);
        
                this.recurringOperationForm.patchValue({
                  account: selectedAccount,
                  name: operation.name,
                  amount: operation.amount,
                  executionTime: operation.executionTime,
                  interval: selectedIntervalForReccuring,
                  startDate: operation.startDate,
                  endDate: operation.endDate
                });

                this.isLoadingForm = true;
              }
            )
          },
          error => {
            console.error('Error retrieving information:', error);
          }
        );
      }
    )   
  }

  UpdateRegOper() {
    if (this.recurringOperationForm.valid) {
      const recPayment: RecurringOperationAction = {
        accountId: this.recurringOperationForm.get('account')!.value.id,
        userId: this.userId,
        name: this.recurringOperationForm.get('name')!.value,
        amount: this.recurringOperationForm.get('amount')!.value,
        startDate: this.recurringOperationForm.get('startDate')!.value,
        endDate: this.recurringOperationForm.get('endDate')!.value,
        interval: this.recurringOperationForm.get('interval')!.value,
        executionTime: this.recurringOperationForm.get('executionTime')!.value,
      };
  
      this.recurringOperationsService.updateOperation(this.regOperId, recPayment).subscribe(
        response => {
          console.log("updated");
        },
        error => {  
        }
      );
    }
  }
}
