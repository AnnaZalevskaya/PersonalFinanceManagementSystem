import { Component, OnInit } from '@angular/core';
import { FinancialAccountsService } from '../../services/financial-accounts.service';
import { Account } from '../../models/account.model';
import { CommonModule } from '@angular/common';
import { NgxPaginationModule } from 'ngx-pagination';

@Component({
  selector: 'app-all-accounts',
  standalone: true,
  imports: [
    CommonModule,
    NgxPaginationModule
  ],
  templateUrl: './all-accounts.component.html',
  styleUrl: './all-accounts.component.css'
})
export class AllAccountsComponent implements OnInit {
  accounts: Account[] = [];

  constructor(private accountService: FinancialAccountsService) { }

  ngOnInit(): void {
    this.loadAccounts();
  }

  loadAccounts() {
    this.accountService.getAccounts().subscribe(
      accounts => {
        this.accounts = accounts;
      },
      error => {
        console.error('Error retrieving accounts:', error);
      }
    );
  }  
}
