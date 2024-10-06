import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FinancialAccountsService } from '../../../services/financial-accounts.service';
import { Account } from '../../../models/account.model';
import { AuthResponse } from '../../../models/auth.model';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';
import { AuthService } from '../../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { PaginationSettings } from '../../../settings/pagination-settings';
import { MatCardModule } from '@angular/material/card';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltip } from '@angular/material/tooltip';
import { MatGridListModule } from '@angular/material/grid-list';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

@Component({
  selector: 'app-profile',
  standalone: true,
  templateUrl: './profile.component.html',
  styleUrl: './profile.component.css',
  imports: [
    MatCardModule,
    MatIconModule,
    MatTooltip,
    MatGridListModule,
    MatPaginatorModule,
    CommonModule, 
    FormsModule,
    RouterModule,
    LoadingIndicatorComponent
  ]
})

export class ProfileComponent implements OnInit {
  isMobile = false;
  isLoadingForm: boolean = false;
  isLoadingAccountForm: boolean = false;

  accounts: Account[] = [];
  user!: AuthResponse;

  paginationSettings: PaginationSettings = new PaginationSettings();
  recordCount: number = 0;

  constructor(
    private router: Router,
    private authService: AuthService,
    private accountService: FinancialAccountsService,
    private breakpointObserver: BreakpointObserver
  ) { }

  ngOnInit(): void {
    this.breakpointObserver.observe([Breakpoints.Handset])
      .subscribe(result => {
        this.isMobile = result.matches;
      }
    );

    this.user = this.authService.getCurrentUser(); 
    
    this.accountService.getUserRecordsCount(this.user.id.toString())
      .subscribe(count => {
        this.recordCount = count;
      }
    ); 
    this.paginationSettings.pageSize = 2;
    this.paginationSettings.pageNumber = 1;
    this.loadData();
    this.isLoadingForm = true;
  }

  loadData() {
    this.isLoadingAccountForm = false;

    this.accountService.getAccountsByUser(this.user.id.toString(), this.paginationSettings).subscribe(
      accounts => {
        this.accounts = accounts; 
        console.log(this.accounts);
        this.isLoadingAccountForm = true;
      },
      error => {
        console.error('Error retrieving accounts:', error);
      }
    );
  }

  redirectToNewAccountPage() {
    const userId = this.user.id;

    this.router.navigate([`/profile/${ userId }/add-account`]); 
  }

  pageEvent!: PageEvent;

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.paginationSettings.pageNumber = e.pageIndex + 1;
    this.loadData();
  }
}
