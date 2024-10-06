import { Component, Inject } from '@angular/core';
import { User } from '../../../models/user.model';
import { Account } from '../../../models/account.model';
import { AuthService } from '../../../services/auth.service';
import { FinancialGoalsService } from '../../../services/financial-goals.service';
import { MAT_BOTTOM_SHEET_DATA, MatBottomSheetModule, MatBottomSheetRef } from '@angular/material/bottom-sheet';
import { Goal } from '../../../models/goal.model';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { MatButtonModule } from '@angular/material/button';

@Component({
  selector: 'app-delete-goal',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule,
    MatButtonModule, 
    MatBottomSheetModule
  ],
  templateUrl: './delete-goal.component.html',
  styleUrl: './delete-goal.component.css'
})
export class DeleteGoalComponent {
  user!: User;
  account!: Account;
  goal: Goal;
  accountId: string | null | undefined;
  showSuccessMessage: boolean = false;

  constructor(
    private authService: AuthService,
    private goalService: FinancialGoalsService,
    @Inject(MAT_BOTTOM_SHEET_DATA) public data: any,
    private bottomSheetRef: MatBottomSheetRef<DeleteGoalComponent>
  ) {
    this.goal = data.goal;
  }

  ngOnInit(): void {
   
  }

  closeGoal() {
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
