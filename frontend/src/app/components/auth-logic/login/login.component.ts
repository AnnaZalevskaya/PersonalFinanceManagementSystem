import { CommonModule } from '@angular/common';
import { HttpClientModule } from '@angular/common/http';
import { Component, EventEmitter, Output } from '@angular/core';
import { MatFormFieldModule } from '@angular/material/form-field';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatIconModule } from '@angular/material/icon';
import { MatCheckboxModule } from '@angular/material/checkbox';
import { Router } from '@angular/router';
import { AuthService } from '../../../services/auth.service';
import { AuthRequest } from '../../../models/auth.model';
import { LoadingIndicatorComponent } from '../../additional-pages/loading-indicator/loading-indicator.component';

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
  @Output() isLoadingFormChange: EventEmitter<boolean> = new EventEmitter<boolean>();
  isLoadingForm: boolean = true;

  errorMessage = '';

  loginForm: FormGroup;

  constructor(private router: Router, private authService: AuthService, private formBuilder: FormBuilder) { 
    this.loginForm = this.formBuilder.group({
      email: ['', [Validators.required, Validators.email]],
      password: ['', Validators.required]
    });
  }

  login() {
    if (this.loginForm.valid) {
      const model: AuthRequest = { 
        email: this.loginForm.get('email')!.value, 
        password: this.loginForm.get('password')!.value 
      };   
      this.toggleLoadingForm();
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

  toggleLoadingForm() {
    this.isLoadingForm = !this.isLoadingForm;
    this.isLoadingFormChange.emit(this.isLoadingForm);
  }

  togglePasswordVisibility() {
    this.hide = !this.hide;
  }
}
