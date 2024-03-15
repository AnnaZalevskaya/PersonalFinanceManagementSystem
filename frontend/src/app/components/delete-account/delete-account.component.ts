import { Component, OnInit } from '@angular/core';
import { FinancialAccountsService } from '../../services/financial-accounts.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { User } from '../../models/user.model';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Account } from '../../models/account.model';

@Component({
  selector: 'app-delete-account',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule
  ],
  templateUrl: './delete-account.component.html',
  styleUrl: './delete-account.component.css'
})
export class DeleteAccountComponent implements OnInit {
  user!: User;
  account!: Account;
  accountId: string | null | undefined;
  showSuccessMessage: boolean = false;

  constructor(
    private route: ActivatedRoute,
    private authService: AuthService,
    private accountService: FinancialAccountsService
  ) {}

  ngOnInit(): void {
    this.accountId = this.route.parent?.snapshot.paramMap.get('id');
    
    if (this.accountId) {
      this.accountService.getAccountById(this.accountId).subscribe(
        account => {
          this.account = account;
        }
      )
    }
  }

  closeAccount() {
    this.user = this.authService.getCurrentUser();
    const userId = this.user.id.toString();
    console.log(this.user + " " + userId);

   /* if (this.accountId) {
      this.accountService.deleteAccount(userId, this.accountId).subscribe(
        response => {*/
          this.showSuccessMessage = true;
   /*     }
      );
    };   */
  }
}
