import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormControl, FormGroup, FormsModule, Validators } from '@angular/forms';
import { AuthService } from '../../services/auth.service';
import { AuthRequest } from '../../models/auth.model';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';
import { RegisterRequest } from '../../models/register.model';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule,
    LoadingIndicatorComponent
  ],
  templateUrl: './auth.component.html',
  styleUrl: './auth.component.css'
})

export class AuthComponent {
  isLoadingForm: boolean = true;
  isRegistrationSuccessful: boolean = false;

  email: string = '';
  username: string = '';
  phone: string = '';
  password: string = '';
  passwordConfirm: string = '';

  constructor(private router: Router, private authService: AuthService) {
}

  register() {
    const model: RegisterRequest = {
      email: this.email,
      username: this.username,
      phoneNumber: this.phone,
      password: this.password,
      passwordConfirm: this.passwordConfirm
    };
    this.authService.register(model).subscribe(
      response => {
        console.log("response: " + response.email + "; " + response.username + "; " + response.phoneNumber);
        this.isRegistrationSuccessful = true;
      }
    )  
  }

  login() {
    const model: AuthRequest = { 
      email: this.email, 
      password: this.password 
    };
    this.isLoadingForm = false;
    this.authService.authenticate(model).subscribe(
      response => {       
        this.authService.saveUser(response);
        this.authService.saveAccessToken(response.token);
        const userId = response.id;
        if (response.role == 'Client') {
          this.router.navigate([`/profile/${userId}`]);
        }
        else if (response.role == 'Admin') {
          this.router.navigate(['admin']);
        }       
      },
      error => {
        console.error('Error during authentication:', error);
      }
    );
  }
}
