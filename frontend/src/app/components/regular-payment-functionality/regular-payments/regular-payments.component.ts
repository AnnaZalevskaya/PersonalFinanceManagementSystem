import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatTooltipModule } from '@angular/material/tooltip';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { IntervalEnum, RecurringOperation } from '../../../models/recurring-payment.model';
import { ScheduleOperationsService } from '../../../services/schedule-recurring-operations.service';
import { PaginationSettings } from '../../../settings/pagination-settings';
import { DeleteRegularPaymentComponent } from '../delete-regular-payment/delete-regular-payment.component';
import { MatBottomSheet } from '@angular/material/bottom-sheet';

@Component({
  selector: 'app-regular-payments',
  standalone: true,
  imports: [
    MatCardModule,
    MatIconModule,
    MatTooltipModule,
    MatPaginatorModule,
    CommonModule,
    RouterModule
  ],
  templateUrl: './regular-payments.component.html',
  styleUrl: './regular-payments.component.css'
})
export class RegularPaymentsComponent {
  operations: RecurringOperation[] = []; 
  totalAccounts: number = 0;
  pageSize: number = 4; 
  pageIndex: number = 0; 

  userId: string;
  recordCount: number = 0;

  paginationSettings: PaginationSettings = new PaginationSettings();

  onPageChange(event: PageEvent) {
    this.pageIndex = event.pageIndex;
  }

  constructor(
    private recOperService: ScheduleOperationsService,
    private route: ActivatedRoute,
    private bottomSheet: MatBottomSheet
  ) {
    this.userId = this.route.parent?.snapshot.paramMap.get('userId') ?? '';

    this.loadOperations();
    this.getCount();
  }

  loadOperations() {
    this.recOperService.getOperationsByUser(this.userId, this.paginationSettings).subscribe(
      regOperations =>{
        this.operations = regOperations;
        this.operations.forEach(
          oper => {
            console.log(oper.intervalType);
            oper.interval = IntervalEnum[oper.intervalType];
          }
        );
      }
    )
  }

  getCount() {
    this.recOperService.getUserRecordsCount(this.userId).subscribe(
      count => {
        this.recordCount = count;
      }
    )
  }

  openCloseRegOperationModal(id: string) {
    this.recOperService.getOperationById(id).subscribe(
      goal =>{
        const bottomSheetRef = this.bottomSheet.open(DeleteRegularPaymentComponent, {
          data: { goal: goal }
        });
      }
    ) 
  }

  pageEvent!: PageEvent;

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.paginationSettings.pageNumber = e.pageIndex + 1;
    this.loadOperations();
  }
}
