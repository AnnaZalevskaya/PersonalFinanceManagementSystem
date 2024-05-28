import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component } from '@angular/core';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormControl, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Router } from '@angular/router';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';
import { AuthService } from '../../services/auth.service';
import { merge } from 'rxjs';
import { AuthRequest } from '../../models/auth.model';

@Component({
  selector: 'app-user-login',
  standalone: true,
  imports: [
    CommonModule,
    HttpClientModule,
    LoadingIndicatorComponent,
    MatFormFieldModule,
    ReactiveFormsModule,
    MatInputModule,
    MatButtonModule,
    MatIconModule,
    MatCheckboxModule
  ],
  templateUrl: './login.component.html',
  styleUrl: './login.component.css'
})
export class LoginComponent {
  hide = true;
  isLoadingForm: boolean = true;
  isRegistrationSuccessful: boolean = false;

  errorMessage = '';

  email = new FormControl('', [Validators.required, Validators.email]);
  password = new FormControl('', [Validators.required]);

  constructor(private router: Router, private authService: AuthService) { 
    merge(this.email.statusChanges, this.email.valueChanges)
      .pipe(takeUntilDestroyed())
      .subscribe(() => this.updateErrorMessage());
  }

  login() {
    if (this.email.value != null && this.password.value != null) {
      const model: AuthRequest = { 
        email: this.email.value, 
        password: this.password.value 
      };   
      this.isLoadingForm = false;
      this.authService.authenticate(model).subscribe(
        response => {       
          this.authService.saveUser(response);
          this.authService.saveRefreshToken(response.refreshToken);
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
    };
  }

  togglePasswordVisibility() {
    this.hide = !this.hide;
  }

  updateErrorMessage() {
    if (this.email.hasError('required')) {
      this.errorMessage = 'You must enter a value';
    } else if (this.email.hasError('email')) {
      this.errorMessage = 'Not a valid email';
    } else {
      this.errorMessage = '';
    }
  }
}
