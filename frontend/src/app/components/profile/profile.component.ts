import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterModule } from '@angular/router';
import { FinancialAccountsService } from '../../services/financial-accounts.service';
import { Account } from '../../models/account.model';
import { AuthResponse } from '../../models/auth.model';
import { LoadingIndicatorComponent } from "../loading-indicator/loading-indicator.component";
import { AuthService } from '../../services/auth.service';
import { FormsModule } from '@angular/forms';
import { SignalRService } from '../../services/signal-r.service';

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
    private accountService: FinancialAccountsService,
    private signalRService: SignalRService) { }

  ngOnInit(): void {
  //  this.signalRService.startConnection();
  
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
    this.isLoadingForm = true;
    this.sendNotification();
  }

  redirectToNewAccountPage() {
    const userId = this.user.id;
//    this.sendNotification();

    this.router.navigate([`/profile/${ userId }/add-account`]); 
  }

  sendNotification() {
    const message = "You're authorized";
  //  this.signalRService.sendNotification(message);
  }
}
