import { Component } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';
import { RegisterRequest } from '../../models/register.model';
import { CommonModule } from '@angular/common';
import { FormControl, FormsModule, Validators } from '@angular/forms';
import { LoadingIndicatorComponent } from '../loading-indicator/loading-indicator.component';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule,
    MatIconModule,
    MatInputModule,
    MatButtonModule,
    MatCheckboxModule,
    LoadingIndicatorComponent
  ],
  templateUrl: './register.component.html',
  styleUrl: './register.component.css'
})
export class RegisterComponent {
  email = new FormControl('', [Validators.required, Validators.email]);
  username = new FormControl('', [Validators.required]);
  phone = new FormControl('', [Validators.required, Validators.pattern('')]);
  password = new FormControl('', [Validators.required]);
  passwordConfirm = new FormControl('', [Validators.required]);

  isRegistrationSuccessful: boolean = false;
  isLoadingForm: boolean = true;
  hidePass = true;
  hideConfPass = true;

  errorMessage = '';

  constructor(private router: Router, private authService: AuthService) { }

  register() {
    if (this.email.value != null && this.username.value != null && this.phone.value != null 
      && this.password.value != null && this.passwordConfirm.value != null) {
      const model: RegisterRequest = {
        email: this.email.value,
        username: this.username.value,
        phoneNumber: this.phone.value,
        password: this.password.value,
        passwordConfirm: this.passwordConfirm.value
      };
      this.authService.register(model).subscribe(
        response => {
          this.isRegistrationSuccessful = true;
        }
      )  
    }
  }

  passwordsDoNotMatch() {
    if (this.password.value !== "" && this.passwordConfirm.value !== "") {
      return this.password !== this.passwordConfirm;
    }

    return false;
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

  togglePasswordVisibility() {
    this.hide = !this.hide;
  }
}
