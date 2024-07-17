import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';
import { FinancialGoalsService } from '../../../services/financial-goals.service';
import { ActionGoal, GoalType } from '../../../models/goal.model';
import { ActivatedRoute, Router } from '@angular/router';
import { provideNativeDateAdapter } from '@angular/material/core';

@Component({
  selector: 'app-update-goal',
  standalone: true,
  imports: [
    MatFormFieldModule, 
    MatSelectModule, 
    FormsModule, 
    ReactiveFormsModule, 
    MatInputModule,
    MatDatepickerModule,
    CommonModule,
    FormsModule,
    LoadingIndicatorComponent
  ],
  providers: [provideNativeDateAdapter()],
  changeDetection: ChangeDetectionStrategy.OnPush,
  templateUrl: './update-goal.component.html',
  styleUrl: './update-goal.component.css'
})
export class UpdateGoalComponent {
  isLoadingForm: boolean = false;

  goalForm: FormGroup;
  goalTypes: { label: string, value: number }[];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private goalsService: FinancialGoalsService
  ) {
    const goalId = this.route.snapshot.paramMap.get('goalId');
    console.log("route " + this.route);
    console.log("id " + goalId);

    this.goalTypes = this.getGoalTypes();

    this.goalForm = this.formBuilder.group({
      name: ['', Validators.required],
      type: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      amount: ['', Validators.required]
    });

    this.selectData(goalId!);

    this.isLoadingForm = true;
  }

  getGoalTypes(): { label: string, value: GoalType }[] {
    return Object.keys(GoalType)
      .map((key: string) => ({
        label: key,
        value: GoalType[key as keyof typeof GoalType]
      }));
  }

  selectData(id: string) {
    this.goalsService.getGoalById(id).subscribe(
      goal => {
        const selectedGoalType = this.goalTypes.find(type => type.label === goal.typeId.toString());

        this.goalForm.patchValue({
          name: goal.name,
          type: selectedGoalType,
          startDate: goal.startDate,
          endDate: goal.endDate,
          amount: goal.amount
        });
      }
    )
  }

  UpdateAccountGoal() {
    if (this.goalForm.valid) {
      const newGoal: ActionGoal = {
        accountId: this.goalForm.get('name')!.value, 
        name: this.goalForm.get('name')!.value,   
        typeId: Number(this.goalForm.get('type')!.value),
        startDate: this.goalForm.get('startDate')!.value.id,  
        endDate: this.goalForm.get('endDate')!.value,
        amount: this.goalForm.get('amount')!.value.id,
      };
  
      this.goalsService.addGoal(newGoal).subscribe(
        response => {
          this.router.navigate(['./']);
          console.log("added");
        },
        error => {  
        }
      );
    }
  }
}
