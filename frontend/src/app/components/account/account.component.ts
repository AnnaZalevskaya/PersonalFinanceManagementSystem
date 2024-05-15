import { Component, OnInit } from '@angular/core';
import { Account } from '../../models/account.model';
import { DatePipe } from '@angular/common';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FinancialAccountsService } from '../../services/financial-accounts.service';
import { OperationsService } from '../../services/operations.service';
import { HttpClientModule } from '@angular/common/http';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';
import { FormsModule } from '@angular/forms';
import { PaginationSettings } from '../../settings/pagination-settings';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
    LoadingIndicatorComponent
  ],
  templateUrl: './account.component.html',
  styleUrl: './account.component.css',
  providers: [DatePipe]
})
export class AccountComponent implements OnInit {
  isLoadingForm: boolean = false;

  account!: Account;
  id!: string;
  
  paginationSettings: PaginationSettings = new PaginationSettings();

  constructor(
    private route: ActivatedRoute, 
    private accountService: FinancialAccountsService,
    private operationsService: OperationsService
  ) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');

    if (id !== null) {
      this.loadAccount(id);
    }
    else {
      (error: any) => {
        console.error('Error during getting id:', error);
      };  
    }

    this.isLoadingForm = true;
  }

  loadAccount(id: string) {
    this.id = id;
    this.accountService.getAccountById(id).subscribe(
      account => {
        this.account = account; 
        this.account.operations = [];
        this.loadOperations(account.id.toString());
      },
      error => {
        console.error('Error retrieving accounts:', error);
      }
    );
  }

  loadOperations(id: string) {
    this.operationsService.getOperationsByAccount(id, this.paginationSettings).subscribe(
      operations =>{
        this.account.operations = operations;
      }
    )
  }

  goToPreviousPage(): void {
    if (this.paginationSettings.pageNumber > 1) {
      this.paginationSettings.pageNumber--;
      this.loadOperations(this.id);
    }
  }

  goToNextPage(): void {
    this.paginationSettings.pageNumber++;
    this.loadOperations(this.id);
  }
}