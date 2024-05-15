import { Component, OnInit } from '@angular/core';
import { FinancialAccountsService } from '../../services/financial-accounts.service';
import { Account } from '../../models/account.model';
import { CommonModule } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';
import { PaginationSettings } from '../../settings/pagination-settings';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';

@Component({
  selector: 'app-all-accounts',
  standalone: true,
  imports: [
    CommonModule,
    NgxPaginationModule,
    LoadingIndicatorComponent
  ],
  templateUrl: './all-accounts.component.html',
  styleUrl: './all-accounts.component.css'
})
export class AllAccountsComponent implements OnInit {
  isLoadingForm: boolean = false;

  accounts: Account[] = [];
  paginationSettings: PaginationSettings = new PaginationSettings();

  constructor(private accountService: FinancialAccountsService) { }

  ngOnInit(): void {
    this.loadAccounts();
    this.isLoadingForm = true;
  }

  loadAccounts() {
    this.accountService.getAccounts(this.paginationSettings).subscribe(
      accounts => {
        this.accounts = accounts;
      },
      error => {
        console.error('Error retrieving accounts:', error);
      }
    );
  } 
  
  goToPreviousPage(): void {
    if (this.paginationSettings.pageNumber > 1) {
      this.paginationSettings.pageNumber--;
      this.loadAccounts();
    }
  }

  goToNextPage(): void {
    this.paginationSettings.pageNumber++;
    this.loadAccounts();
  }
}
