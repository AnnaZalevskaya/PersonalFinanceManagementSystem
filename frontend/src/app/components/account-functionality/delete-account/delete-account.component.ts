import { Component, Inject, OnInit } from '@angular/core';
import { FinancialAccountsService } from '../../../services/financial-accounts.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { AuthService } from '../../../services/auth.service';
import { User } from '../../../models/user.model';
import { ActivatedRoute, Router, RouterModule } from '@angular/router';
import { Account } from '../../../models/account.model';
import { OperationsService } from '../../../services/operations.service';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheet, MatBottomSheetModule, MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { MatListModule } from '@angular/material/list';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-delete-account',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    MatButtonModule, 
    MatBottomSheetModule
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
    private router: Router,
    private authService: AuthService,
    private accountService: FinancialAccountsService,
    private operationsService: OperationsService,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: any,
    private bottomSheetRef: MatBottomSheetRef<DeleteAccountComponent>
  ) {
    this.account = data.account;
  }

  ngOnInit(): void {
   
  }

  closeAccount() {
    this.user = this.authService.getCurrentUser();
    const userId = this.user.id.toString();
    console.log(this.user + " " + userId);

    if (this.accountId) {
      this.accountService.deleteAccount(userId, this.accountId).subscribe(
        response => {
          console.log("deleted");
          this.operationsService
            .deleteAccountOperations(this.accountId!).subscribe(
              response => {
                console.log("complete deleted");
              }
            );
        },
        error => {  
          this.router.navigate(['internal-server-error']);
          console.log("Something went wrong...");
        }
      );
      
      this.showSuccessMessage = true;

      this.bottomSheetRef.dismiss();
      this.router.navigate(['./profile/{{userId}}']);
    };      
  }

  back() {
    this.bottomSheetRef.dismiss();
  }
}
