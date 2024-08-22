import { CommonModule, DatePipe } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { ThemePalette } from '@angular/material/core';
import { MatIconModule } from '@angular/material/icon';
import { ProgressBarMode, MatProgressBarModule } from '@angular/material/progress-bar';
import { MatTooltip } from '@angular/material/tooltip';
import { FinancialGoalsService } from '../../../services/financial-goals.service';
import { PaginationSettings } from '../../../settings/pagination-settings';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Goal, GoalType } from '../../../models/goal.model';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { DeleteGoalComponent } from '../delete-goal/delete-goal.component';
import { MatBottomSheet } from '@angular/material/bottom-sheet';
import { MatGridListModule, MatGridTile } from '@angular/material/grid-list';
import { BreakpointObserver, Breakpoints } from '@angular/cdk/layout';

@Component({
  selector: 'app-account-goals',
  standalone: true,
  imports: [
    MatCardModule, 
    MatProgressBarModule,
    CommonModule,
    RouterModule,
    MatIconModule,
    MatTooltip,
    MatPaginatorModule,
    MatGridListModule,
    MatGridTile
  ],
  providers: [DatePipe],
  templateUrl: './account-goals.component.html',
  styleUrl: './account-goals.component.css'
})
export class AccountGoalsComponent implements OnInit {
  isMobile = false;
  color: ThemePalette = 'primary';
  mode: ProgressBarMode = 'determinate';
  id!: string;

  paginationSettings: PaginationSettings = new PaginationSettings();
  recordCount: number = 0;

  financialGoals: Goal[] = [];

  constructor(
    private route: ActivatedRoute, 
    private goalsService: FinancialGoalsService,
    private bottomSheet: MatBottomSheet,
    private breakpointObserver: BreakpointObserver
  ) { 
    this.breakpointObserver.observe([Breakpoints.Handset])
      .subscribe(result => {
        this.isMobile = result.matches;
      }
    );
  }

  ngOnInit(): void {
    this.id = this.route.parent?.snapshot.paramMap.get('id') ?? '';
    console.log("id " + this.id);

    this.paginationSettings.pageSize = 4;

    this.getCount();
    this.loadGoals();
  }

  calculateProgress(goal: Goal): number {
    return (goal.progress / goal.amount) * 100;
  }

  loadGoals() {
    this.goalsService.getGoalsByAccount(this.id, this.paginationSettings).subscribe(
      goals =>{
        this.financialGoals = goals;
        this.financialGoals.forEach(
          goal => {
            goal.type = GoalType[goal.typeId];
          }
        );
      }
    )
  }

  getCount() {
    this.goalsService.getAccountRecordsCount(this.id).subscribe(
      count => {
        this.recordCount = count;
      }
    )
  }

  openCloseAccountGoalModal(id: string) {
    this.goalsService.getGoalById(id).subscribe(
      goal =>{
        const bottomSheetRef = this.bottomSheet.open(DeleteGoalComponent, {
          data: { goal: goal }
        });
      }
    ) 
  }

  pageEvent!: PageEvent;

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.paginationSettings.pageNumber = e.pageIndex + 1;
    this.loadGoals();
  }
}
