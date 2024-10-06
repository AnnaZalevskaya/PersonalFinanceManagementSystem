import { Component, Inject } from '@angular/core';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { FinancialGoalsService } from '../../../services/financial-goals.service';
import { AuthService } from '../../../services/auth.service';
import { User } from '../../../models/user.model';
import { Account } from '../../../models/account.model';
import { RecurringOperation } from '../../../models/recurring-payment.model';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-delete-regular-payment',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './delete-regular-payment.component.html',
  styleUrl: './delete-regular-payment.component.css'
})
export class DeleteRegularPaymentComponent {
  user!: User;
  account!: Account;
  operation: RecurringOperation;
  accountId: string | null | undefined;
  showSuccessMessage: boolean = false;

  constructor(
    private authService: AuthService,
    private goalService: FinancialGoalsService,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: any,
    private bottomSheetRef: MatBottomSheetRef<DeleteRegularPaymentComponent>
  ) {
    this.operation = data.operation;
  }

  ngOnInit(): void {
   
  }

  cancelOperation() {
    this.user = this.authService.getCurrentUser();
    const userId = this.user.id.toString();
    console.log(this.user + " " + userId);

    if (this.accountId) {
      this.goalService.deleteGoal(userId, this.accountId).subscribe();
      this.showSuccessMessage = true;
    };   
  }

  back() {
    this.bottomSheetRef.dismiss();
  }
}
