import { Component, OnInit } from '@angular/core';
import { Account } from '../../../models/account.model';
import { DatePipe, CommonModule } from '@angular/common';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { FinancialAccountsService } from '../../../services/financial-accounts.service';
import { OperationsService } from '../../../services/operations.service';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';
import { FormsModule } from '@angular/forms';
import { PaginationSettings } from '../../../settings/pagination-settings';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { MatDialog } from '@angular/material/dialog';
import { DeleteAccountComponent } from '../delete-account/delete-account.component';
import { MatCardModule } from '@angular/material/card';
import { MatDividerModule } from '@angular/material/divider';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltip } from '@angular/material/tooltip';
import { PdfReportService } from '../../../services/pdf-report.service';
import { OperationCategoriesService } from '../../../services/operation-categories.service';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';
import { Observable } from 'rxjs';
import { OperationCategory } from '../../../models/operation-category.model';
import { UserNotificationsService } from '../../../services/user-notifications.service';
import { ReportSavedNotification } from '../../../models/user-notification.model';
import { DialogSavingReportComponent } from '../dialog-saving-report/dialog-saving-report.component';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    MatCardModule,
    MatDividerModule,
    MatGridListModule,
    MatPaginatorModule,
    MatIconModule,
    MatTooltip,
    CommonModule, 
    FormsModule,
    RouterModule,
    LoadingIndicatorComponent
  ],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css',
  providers: [DatePipe]
})
export class AccountComponent implements OnInit {
  isMobile = false;
  isLoadingForm: boolean = false;
  isLoadingOperationForm: boolean = false;
  isGeneratingReport: boolean = false;
  isDone = false;

  account!: Account;
  id!: string;

  recordCount: number = 0;
  paginationSettings: PaginationSettings = new PaginationSettings();

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private accountService: FinancialAccountsService,
    private operationsService: OperationsService,
    private categoryService: OperationCategoriesService,
    private pdfService: PdfReportService,
    private userNotificationsService: UserNotificationsService,
    private bottomSheet: MatBottomSheet,
    private breakpointObserver: BreakpointObserver,
    public dialog: MatDialog
  ) { 
    this.breakpointObserver.observe([Breakpoints.Handset])
      .subscribe(result => {
        this.isMobile = result.matches;
      }
    );
  }

  ngOnInit(): void { 
    const id = this.route.snapshot.paramMap.get('id');

    if (id !== null) {
      this.loadAccount(id);

      this.operationsService.getAccountRecordsCount(id)
        .subscribe(count => {
          this.recordCount = count;
        }
      ); 
    }
    else {
      (error: any) => {
        console.error('Error during getting id:', error);
      };  
    }
    
    this.paginationSettings.pageSize = 2;

    this.isLoadingForm = true;
  }

  loadAccount(id: string) {
    this.isLoadingOperationForm = false;

    this.id = id;
    this.accountService.getAccountById(id).subscribe(
      account => {
        this.account = account; 
        this.account.operations = [];
        this.loadOperations(account.id.toString());

        this.isLoadingOperationForm = true;
      },
      error => {
        console.error('Error retrieving accounts:', error);
      }
    );
  }

  loadOperations(id: string) {
    this.operationsService.getOperationsByAccount(id, this.paginationSettings).subscribe(
      operations => {
        this.account.operations = operations;

        this.account.operations.forEach(
          operation => {

          const keys = Object.keys(operation.description);

          keys.forEach(key => {
            if (this.isCategory(key)) {
              this.showCategory(operation.description[key]).subscribe(category => {
                const newKey = 'Category';
                operation.description[newKey] = category.name;
                delete operation.description[key];              
              });
            }
          });
        });
      }
    )
  }

  pageEvent!: PageEvent;

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.paginationSettings.pageNumber = e.pageIndex + 1;
    this.loadOperations(this.id);
  }

  openCloseAccountModal() {
    const bottomSheetRef = this.bottomSheet.open(DeleteAccountComponent, {
      data: { account: this.account }
    });
  }

  SaveReport(): void {
    const dialogRef = this.dialog.open(DialogSavingReportComponent, {
      data: { isDone: this.isDone }
    }); 
    //this.dialog.open(DialogSavingReportComponent);

    this.isGeneratingReport = true;
    this.pdfService.generateFullReport(this.account.id.toString()).subscribe(
      response => {
        console.log("saved");
        const notification: ReportSavedNotification = {
          userId: this.account.id.toString(),
          reportName: `FinancialAccount_${ this.account.id.toString() }.pdf`
        };
        //this.userNotificationsService.addReportSavedNotification(notification);

        console.log("old value " + dialogRef.componentInstance.isDone)
        this.isGeneratingReport = false;
        this.isDone = true;
        dialogRef.componentInstance.isDone = this.isDone;
        console.log("new value " + dialogRef.componentInstance.isDone)
      },
      error => {  
        this.router.navigate(['internal-server-error']);
        console.log("Something went wrong...");
      }
    )
  }

  isCategory(key: string) {
    return key === 'CategoryId';
  }

  showCategory(id: string): Observable<OperationCategory> {
    return this.categoryService.getCategoryById(id);
  }
}