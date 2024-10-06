import { CommonModule } from '@angular/common';
import { ChangeDetectionStrategy, Component } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatSelectModule } from '@angular/material/select';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';
import { ActivatedRoute, Router } from '@angular/router';
import { ActionGoal, GoalType } from '../../../models/goal.model';
import { FinancialGoalsService } from '../../../services/financial-goals.service';
import { provideNativeDateAdapter } from '@angular/material/core';

@Component({
  selector: 'app-add-goal',
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
  templateUrl: './add-goal.component.html',
  styleUrl: './add-goal.component.css'
})
export class AddGoalComponent {
  isLoadingForm: boolean = false;
  accountId: string;

  goalForm: FormGroup;
  goalTypes: { label: string, value: number }[];

  constructor(
    private router: Router,
    private route: ActivatedRoute,
    private formBuilder: FormBuilder,
    private goalsService: FinancialGoalsService
  ) {
    this.accountId = this.route.parent?.snapshot.paramMap.get('id') ?? '';

    this.goalTypes = this.getGoalTypes();

    this.goalForm = this.formBuilder.group({
      name: ['', Validators.required],
      type: ['', Validators.required],
      startDate: ['', Validators.required],
      endDate: ['', Validators.required],
      amount: ['', Validators.required]
    });

    this.goalTypes.forEach(element => {
      console.log(element.value + " " + element.label);
    });

    this.isLoadingForm = true;
  }

  getGoalTypes(): { label: string, value: GoalType }[] {
    return Object.keys(GoalType)
      .map((key: string) => ({
        label: key,
        value: GoalType[key as keyof typeof GoalType]
      }));
  }

  AddAccountGoal() {
    if (this.goalForm.valid) {
      const newGoal: ActionGoal = {
        accountId: this.accountId, 
        name: this.goalForm.get('name')!.value,   
        typeId: this.goalForm.get('type')!.value.label,
        startDate: this.goalForm.get('startDate')!.value,  
        endDate: this.goalForm.get('endDate')!.value,
        amount: this.goalForm.get('amount')!.value,
      };

      console.log("New goal " +
        newGoal.accountId + " | " +
        newGoal.name + " | " +
        newGoal.typeId + " | " + 
        newGoal.startDate + " | " + 
        newGoal.endDate + " | " + 
        newGoal.amount 
      );
  
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
