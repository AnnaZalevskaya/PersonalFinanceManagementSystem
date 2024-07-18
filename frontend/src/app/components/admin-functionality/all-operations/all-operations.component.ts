import { CommonModule, DatePipe } from '@angular/common';
import { Component } from '@angular/core';
import { MatCardModule } from '@angular/material/card';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { OperationsService } from '../../../services/operations.service';
import { PaginationSettings } from '../../../settings/pagination-settings';
import { Operation } from '../../../models/operation.model';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Router } from '@angular/router';

@Component({
  selector: 'app-all-operations',
  standalone: true,
  imports: [
    MatPaginatorModule,
    MatCardModule, 
    CommonModule,
    LoadingIndicatorComponent,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule
  ],
  templateUrl: './all-operations.component.html',
  styleUrl: './all-operations.component.css',
  providers: [DatePipe]
})
export class AllOperationsComponent {
  isLoadingOperationForm: boolean = false;

  operations: Operation[] = [];

  recordCount: number = 0;
  paginationSettings: PaginationSettings = new PaginationSettings();

  constructor(private operationsService: OperationsService, private router: Router) { }

  ngOnInit(): void { 
    this.operationsService.getRecordsCount()
      .subscribe(count => {
        this.recordCount = count;
      }
    ); 
    
    this.paginationSettings.pageSize = 3;
    this.paginationSettings.pageNumber = 1;
  }
  
  pageEvent!: PageEvent;

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.paginationSettings.pageNumber = e.pageIndex + 1;
    this.loadOperations();
  }

  loadOperations() {
    this.operationsService.getOperations(this.paginationSettings).subscribe(
      operations =>{
        this.operations = operations;
        this.isLoadingOperationForm = true;
      }
    )
  }

  back() {
    this.router.navigate(['./admin']);
  }
}
