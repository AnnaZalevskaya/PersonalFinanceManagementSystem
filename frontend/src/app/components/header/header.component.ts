import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../services/auth.service';
import { NavigationEnd, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { UserNotificationsService } from '../../services/user-notifications.service';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    RouterModule
  ],
  templateUrl: './header.component.html',
  styleUrl: './header.component.css'
})
export class HeaderComponent implements OnInit {
  username: string = '';
  isAuthenticated!: boolean;
  headerText: string  = '';

  unreadCount: number = 0;
  userId: number = 0;

  constructor(
    private router: Router,  
    private authService: AuthService, 
    private notificationsService: UserNotificationsService
  ) { 
    
  }
  ngOnInit(): void {
    this.router.events.subscribe(event => {
      if (event instanceof NavigationEnd) {
        this.isAuthenticated = this.authService.isLoggedIn();
        this.userId = this.authService.getCurrentUser()['id'];
        this.username = this.authService.getCurrentUser()['username'];
      }
    })
  }

  login() {
    this.router.navigate(['/auth']); 
  }

  logout() {
    this.authService.logOut();
    this.isAuthenticated = this.authService.isLoggedIn();
    this.router.navigate(['/main']); 
  }

  getUserName() {
    return 'Welcome, ' + this.username;
  }

  isClient() {
    if(this.authService.getCurrentUser()['role'] == 'Client') {
      return true;
    }
    else {
      return false;
    }
  }
}
