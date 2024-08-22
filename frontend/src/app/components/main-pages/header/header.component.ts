import { Component, OnInit } from '@angular/core';
import { AuthService } from '../../../services/auth.service';
import { NavigationEnd, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Router } from '@angular/router';
import { NotificationPanelComponent } from '../../additional-pages/notification-panel/notification-panel.component';
import { NotificationPanelService } from '../../../services/notification-panel.service';
import { MatIconModule } from '@angular/material/icon';
import { MatBadgeModule } from '@angular/material/badge';
import { MatMenuModule } from '@angular/material/menu';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-header',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    RouterModule, 
    MatButtonModule, 
    MatMenuModule, 
    MatIconModule,
    MatBadgeModule,
    NotificationPanelComponent
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
    private panelService: NotificationPanelService
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
    return this.username;
  }

  isClient() {
    if(this.authService.getCurrentUser()['role'] == 'Client') {
      return true;
    }
    else {
      return false;
    }
  }

  toProfile() {
    if(this.isClient()) {
      this.router.navigate([`/profile/${this.userId}`]); 
    }
    else {
      this.router.navigate(['/admin']); 
    }
  }

  toRegularPaymentPage() {
    this.router.navigate([`/profile/${this.userId}/regular-payments`]); 
  }

  toAccountStatictics() {
    this.router.navigate(['/admin/show-statistic']); 
  }

  toggleNotificationPanel() {
    this.panelService.openPanel();
  }

  showNotificationPanel() {
    return this.panelService.getStatus();
  }
}
