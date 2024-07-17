import { Component } from '@angular/core';
import { User } from '../../../models/user.model';
import { UsersService } from '../../../services/users.service';
import { CommonModule } from '@angular/common';
import { PaginationSettings } from '../../../settings/pagination-settings';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';
import { MatPaginatorModule, PageEvent } from '@angular/material/paginator';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';
import { Router } from '@angular/router';

@Component({
  selector: 'app-all-users',
  standalone: true,
  imports: [
    CommonModule,
    LoadingIndicatorComponent,
    MatCardModule,
    MatPaginatorModule,
    MatButtonModule,
    MatIconModule,
    MatTooltipModule
  ],
  templateUrl: './all-users.component.html',
  styleUrl: './all-users.component.css'
})
export class AllUsersComponent {
  isLoadingForm: boolean = false;

  users: User[] = [];
  paginationSettings: PaginationSettings = new PaginationSettings();
  recordCount: number = 0;

  constructor(private usersService: UsersService, private router: Router) { }

  ngOnInit() {
    this.usersService.getRecordsCount()
      .subscribe(count => {
        this.recordCount = count;
      }
    );

    this.paginationSettings.pageSize = 3;
    this.paginationSettings.pageNumber = 1;

    this.loadUsers();
    this.isLoadingForm = true;
  }

  loadUsers() {
    this.usersService.getUsers(this.paginationSettings).subscribe(
      users => {
        this.users = users;
      },
      error => {
        console.error('Error retrieving users:', error);
      }
    );
  }  

  pageEvent!: PageEvent;

  handlePageEvent(e: PageEvent) {
    this.pageEvent = e;
    this.paginationSettings.pageNumber = e.pageIndex + 1;
    this.loadUsers();
  }

  back() {
    this.router.navigate(['./admin']);
  }
}
