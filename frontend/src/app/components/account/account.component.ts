import { Component, OnInit } from '@angular/core';
import { Account } from '../../models/account.model';
import { DatePipe } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FinancialAccountsService } from '../../services/financial-accounts.service';
import { OperationsService } from '../../services/operations.service';
import { HttpClientModule } from '@angular/common/http';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';

@Component({
  selector: 'app-account',
  standalone: true,
  imports: [
    CommonModule,
    HttpClientModule,
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

  constructor(private route: ActivatedRoute, 
    private accountService: FinancialAccountsService,
    private operationsService: OperationsService) { }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id !== null) {
      this.id = id;
      this.accountService.getAccountById(id).subscribe(
        account => {
          this.account = account; 
          this.operationsService.getOperationsByAccount(account.id.toString()).subscribe(
            operations =>{
              account.operations = operations;
            }
          )
        },
        error => {
          console.error('Error retrieving accounts:', error);
        }
      );
      }
     else {
      (error: any) => {
        console.error('Error during getting id:', error);
      };
      this.isLoadingForm = true;
    }
  }
}