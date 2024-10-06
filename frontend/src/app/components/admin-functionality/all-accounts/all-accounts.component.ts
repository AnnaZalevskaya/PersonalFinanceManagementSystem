import { Component, OnInit } from '@angular/core';
import { FinancialAccountsService } from '../../../services/financial-accounts.service';
import { Account } from '../../../models/account.model';
import { CommonModule } from '@angular/common';
import { PaginationSettings } from '../../../settings/pagination-settings';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { Router } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-all-accounts',
  standalone: true,
  imports: [
    CommonModule,
    LoadingIndicatorComponent,
    MatCardModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule
  ],
  templateUrl: './all-accounts.component.html',
  styleUrl: './all-accounts.component.css'
})
export class AllAccountsComponent implements OnInit {
  isLoadingAccountForm: boolean = false;

  accounts: Account[] = [];
  paginationSettings: PaginationSettings = new PaginationSettings();
  recordCount: number = 0;

  constructor(private accountService: FinancialAccountsService, private router: Router) { }

  ngOnInit(): void {
    this.accountService.getRecordsCount()
      .subscribe(count => {
        this.recordCount = count;
      }
    ); 

    this.paginationSettings.pageSize = 3;
    this.paginationSettings.pageNumber = 1;

    this.loadAccounts();
  }

  loadAccounts() {
    this.accountService.getAccounts(this.paginationSettings).subscribe(
      accounts => {
        this.accounts = accounts;
        this.isLoadingAccountForm = true;
      },
      error => {
        console.error('Error retrieving accounts:', error);
      }
    );
  } 
  
  pageEvent!: PageEvent;

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.paginationSettings.pageNumber = e.pageIndex + 1;
    this.loadAccounts();
  }

  back() {
    this.router.navigate(['./admin']);
  }
}
