import { Component } from '@angular/core';
import { User } from '../../models/user.model';
import { UsersService } from '../../services/users.service';
import { CommonModule } from '@angular/common';
import { PaginationSettings } from '../../settings/pagination-settings';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';

@Component({
  selector: 'app-all-users',
  standalone: true,
  imports: [
    CommonModule,
    LoadingIndicatorComponent
  ],
  templateUrl: './all-users.component.html',
  styleUrl: './all-users.component.css'
})
export class AllUsersComponent {
  isLoadingForm: boolean = false;

  users: User[] = [];
  paginationSettings: PaginationSettings = new PaginationSettings();

  constructor(private usersService: UsersService) { }

  ngOnInit() {
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

  goToPreviousPage(): void {
    if (this.paginationSettings.pageNumber > 1) {
      this.paginationSettings.pageNumber--;
      this.loadUsers();
    }
  }

  goToNextPage(): void {
    this.paginationSettings.pageNumber++;
    this.loadUsers();
  }
}
