import { Component } from '@angular/core';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { AuthService } from '../../../services/auth.service';
import { RegisterRequest } from '../../../models/register.model';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';
import { MatIconModule } from '@angular/material/icon';

@Component({
  selector: 'app-register',
  standalone: true,
  imports: [
    CommonModule, 
    FormsModule,
    ReactiveFormsModule,
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
  registrationForm: FormGroup;

  isRegistrationSuccessful: boolean = false;
  isLoadingForm: boolean = true;
  hidePass = true;
  hideConfPass = true;

  constructor(private authService: AuthService, private formBuilder: FormBuilder) { 
    this.registrationForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      userName: ['', [Validators.required]],
      phone: ['', [Validators.required]],
      password: ['', Validators.required],
      confirmPassword: ['', Validators.required]  
    });
  }

  register() {
    if (this.registrationForm.valid) {
      const model: RegisterRequest = {
        email: this.registrationForm.get('email')!.value,
        username: this.registrationForm.get('userName')!.value,
        phoneNumber: this.registrationForm.get('phone')!.value,
        password: this.registrationForm.get('password')!.value,
        passwordConfirm: this.registrationForm.get('confirmPassword')!.value
      };
      
      this.authService.register(model).subscribe(
        response => {
          this.isRegistrationSuccessful = true;
        }
      )  
    }
  }

  togglePasswordVisibility() {
    this.hidePass = !this.hidePass;
  }

  toggleConfirmPasswordVisibility() {
    this.hideConfPass = !this.hideConfPass;
  }
}
