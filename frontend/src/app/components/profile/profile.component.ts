import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FinancialAccountsService } from '../../services/financial-accounts.service';
import { Account } from '../../models/account.model';
import { AuthResponse } from '../../models/auth.model';
import { LoadingIndicatorComponent } from "../loading-indicator/loading-indicator.component";
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';

@Component({
    selector: 'app-profile',
    standalone: true,
    templateUrl: './profile.component.html',
    styleUrl: './profile.component.css',
    imports: [
      CommonModule, 
      FormsModule,
      RouterModule,
      LoadingIndicatorComponent
    ]
})

export class ProfileComponent implements OnInit {
  isLoadingForm: boolean = false;

  accounts: Account[] = [];
  user!: AuthResponse;

  constructor(private router: Router,
    private authService: AuthService,
    private accountService: FinancialAccountsService) { }

  ngOnInit(): void {
    this.loadData();
    this.isLoadingForm = true;
  }

  loadData() {
    this.user = this.authService.getCurrentUser(); 
    console.log(this.user);
    this.accountService.getAccountsByUser(this.user.id.toString()).subscribe(
      accounts => {
        this.accounts = accounts; 
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
}
